# Distro
This repository will house a collection of shared libraries that will be distributed in NuGet that should help quickly launch projects I would like to play around with.

## Problem Details

Nuget package:
```
dotnet add package gitViwe.ProblemDetail --version 1.1.0
```

ProblemDetailFactory static class:

```csharp
public IActionResult Result()
{
    var extensionValue = new
    {
        Balance = 30.0m,
        Accounts = { "/account/12345", "/account/67890" }
    };

    var problem = ProblemDetailFactory.CreateProblemDetails(
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
ProblemDetailFactory from the exception handler:

```csharp
internal static void UseHubExceptionHandler(this IApplicationBuilder app, ILogger logger)
{
    app.UseExceptionHandler(options =>
    {
        options.Run(async context =>
        {
            var handlerFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            int statusCode = StatusCodes.Status500InternalServerError;
            string response = JsonSerializer.Serialize(ProblemDetailFactory.CreateProblemDetails(context, statusCode));

            if (handlerFeature is not null && handlerFeature.Error is ValidationException validation)
            {
                statusCode = StatusCodes.Status400BadRequest;
                response = JsonSerializer.Serialize(ProblemDetailFactory.CreateValidationProblemDetails(context, statusCode, validation.ToDictionary()));
                logger.Log(LogLevel.Information, validation, "A validation exception occurred. Problem detail: {response}", response);
            }

            await context.Response.WriteAsync(response);
            await context.Response.CompleteAsync();
        });
    });
}
```