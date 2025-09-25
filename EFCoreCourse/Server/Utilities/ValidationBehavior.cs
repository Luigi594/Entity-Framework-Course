using FluentValidation;
using MediatR;

namespace EFCoreCourse.Server.Utilities
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next(cancellationToken);
            }

            var context = new ValidationContext<TRequest>(request);

            // Use 'ValidateAsync' and 'Task.WhenAll' to run all validations
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            // Get all errors from all validators.
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                // If there are validation failures, we throw an exception.
                throw new ValidationException(failures);
            }

            // If everything is valid, we continue to the next step in the pipeline (the handler).
            return await next(cancellationToken);
        }
    }
}
