namespace gitViwe.Shared.MediatR.Behaviour;

/// <summary>
/// Creates a validation pipeline for validating MediatR requests that have registered Fluent validation validators
/// </summary>
/// <typeparam name="TRequest">The MediatR request type</typeparam>
/// <typeparam name="TResponse">The MediatR response type</typeparam>
[Obsolete("ValidationBehaviour is deprecated, please use ValidationPreProcessor instead.")]
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Creates a new instance of <see cref="ValidationBehaviour{TRequest, TResponse}"/>
    /// </summary>
    /// <param name="validators">A collection of the registered validators</param>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Runs all the validators for the given MediatR request
    /// </summary>
    /// <param name="request">The MediatR request command or query</param>
    /// <param name="next">The delegate to handle the next function in the pipeline</param>
    /// <param name="cancellationToken">The Cancellation token to notify that operations should be canceled</param>
    /// <returns>The next MediatR function in the pipeline</returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
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
        return await next();
    }
}
