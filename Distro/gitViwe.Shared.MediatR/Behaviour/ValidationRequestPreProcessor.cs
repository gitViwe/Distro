namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a validation pipeline for validating MediatR requests that have registered Fluent validation validators
/// </summary>
/// <typeparam name="TRequest">The MediatR request type</typeparam>
/// <typeparam name="TResponse">The MediatR response type</typeparam>
public class ValidationRequestPreProcessor<TRequest, TResponse> : IRequestPreProcessor<TRequest>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Creates a new instance of <see cref="ValidationRequestPreProcessor{TRequest, TResponse}"/>
    /// </summary>
    /// <param name="validators">A collection of the registered validators</param>
    public ValidationRequestPreProcessor(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
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
