namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a validation pipeline for validating MediatR requests that have registered Fluent validation validators
/// </summary>
/// <typeparam name="TRequest">The MediatR notification type</typeparam>
public class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationPreProcessor<TRequest>> _logger;

    /// <summary>
    /// Creates a new instance of <see cref="ValidationPreProcessor{TRequest}"/>
    /// </summary>
    /// <param name="validators">A collection of the registered validators</param>
    /// <param name="logger">The logger</param>
    public ValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationPreProcessor<TRequest>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    /// <summary>
    /// Runs all the validators for the given MediatR <paramref name="request"/>
    /// </summary>
    /// <param name="request">The MediatR request command or query</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    /// <returns>A <see cref="Task"/> that represents the operation</returns>
    /// <exception cref="ValidationException"></exception>
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        OpenTelemetryActivity.MediatR.StartActivity("Validation MediatR PreProcessor", "Starting MediatR Request.");

        if (_validators is not null && _validators.Any())
        {
            _logger.LogInformation("Starting request validation. {request} using {validators}", request.GetType().Name, _validators.Select(x => x.GetType().Name));

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            if (validationResults is not null)
            {
                var failure = validationResults.Where(result => result.IsValid == false).FirstOrDefault();

                if (failure is not null) throw new ValidationException(failure.ToDictionary());
            }
        }
    }
}
