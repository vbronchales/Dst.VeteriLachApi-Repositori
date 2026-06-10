using FluentValidation;
using MediatR;

namespace VeteriLach.ReadApi.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior per validar requests amb FluentValidation
/// </summary>
public partial class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count > 0)
        {
            var requestName = typeof(TRequest).Name;
            LogValidationFailed(requestName, string.Join("; ", failures.Select(f => f.ErrorMessage)));

            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }

    [LoggerMessage(EventId = 0, Level = LogLevel.Warning, Message = "Validació fallida per a {RequestName}. Errors: {Errors}")]
    partial void LogValidationFailed(string requestName, string errors);
}
