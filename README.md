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
