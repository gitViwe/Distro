namespace gitViwe.Shared.FluentValidation.MediatR;

/// <summary>
/// Creates a validation pipeline for validating MediatR requests that have registered Fluent validation validators
/// </summary>
/// <typeparam name="TRequest">The MediatR notification type</typeparam>
/// <remarks>
/// Creates a new instance of <see cref="FluentValidationPreProcessor{TRequest}"/>
/// </remarks>
/// <param name="validators">A collection of the registered validators</param>
public class FluentValidationPreProcessor<TRequest>(IEnumerable<IValidator<TRequest>> validators) : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    /// <summary>
    /// Runs all the validators for the given MediatR <paramref name="request"/>
    /// </summary>
    /// <param name="request">The MediatR request command or query</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be cancelled</param>
    /// <returns>A <see cref="Task"/> that represents the operation</returns>
    /// <exception cref="ValidationException"></exception>
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        Dictionary<string, object?> requestTagDictionary = new()
        {
            { OpenTelemetryTagKey.MediatR.REQUEST_TYPE, request.GetType().Name },
            { OpenTelemetryTagKey.MediatR.REQUEST_VALIDATOR, string.Join('|', _validators.Select(x => x.GetType().Name)) },
        };

        OpenTelemetryActivity.MediatR.StartActivity("Validation MediatR PreProcessor", "Starting Request Validation.", requestTagDictionary);

        if (_validators is not null && _validators.Any())
        {
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
