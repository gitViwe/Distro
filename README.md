# Distro
This repository will house a collection of shared libraries that will be distributed in NuGet that should help quickly launch projects I would like to play around with.

## Problem Details

Register the service:

```csharp
// Add Custom Problem Detail Factory inside ConfigureServices
services.AddCustomProblemDetailFactory();
```
IProblemDetailFactory from the controller:

```csharp
// Inject and use 'IProblemDetailFactory' interface
public IActionResult Result([FromServices] IProblemDetailFactory problemDetailFactory)
{
    var extensionValue = new
    {
        Balance = 30.0m,
        Accounts = { "/account/12345", "/account/67890" }
    };

    var problem = problemDetailFactory.CreateProblemDetails(
                    context: HttpContext,
                    statusCode: StatusCodes.Status412PreconditionFailed,
                    extensions: new Dictionary<string, object?>()
                    {
                        { "outOfCredit", extensionValue }
                    },
                    detail: "Your current balance is 30, but that costs 50.");

    return StatusCode(problem.Status!.Value, problem);
}
```
IProblemDetailFactory from the exception handler:

```csharp
// Resolve and use 'IProblemDetailFactory' interface
internal static void UseHubExceptionHandler(this IApplicationBuilder app, ILogger logger, IServiceProvider serviceProvider)
{
    app.UseExceptionHandler(options =>
    {
        options.Run(async context =>
        {
            // default error status code
            int statusCode = StatusCodes.Status500InternalServerError;
            // resolve 'IProblemDetailFactory' from the DI container
            var problemDetailsFactory = serviceProvider.GetRequiredService<IProblemDetailFactory>();
            // default response content
            string response = JsonSerializer.Serialize(problemDetailsFactory.CreateProblemDetails(context, statusCode));

            var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            // override response based on exception
            if (exceptionFeature.Error is ValidationException validationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                response = JsonSerializer.Serialize(problemDetailsFactory.CreateValidationProblemDetails(
                                                                            context,
                                                                            statusCode,
                                                                            validationException.ToDictionary()));
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(response);
            await context.Response.CompleteAsync();
        });
    });
}
```