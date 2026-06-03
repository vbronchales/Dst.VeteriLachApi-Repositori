using MediatR;
using System.Diagnostics;

namespace VeteriLach.ReadApi.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior per registrar el temps d'execució de cada request
/// </summary>
public partial class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        LogExecutingRequest(requestName);
        try
        {
            var response = await next();
            stopwatch.Stop();

            LogCompletedRequest(requestName, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            LogErrorRequest(requestName, stopwatch.ElapsedMilliseconds, ex);
            throw;
        }
    }

    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Executant {RequestName}")]
    partial void LogExecutingRequest(string requestName);

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Completat {RequestName} en {ElapsedMilliseconds}ms")]
    partial void LogCompletedRequest(string requestName, long elapsedMilliseconds);
    [LoggerMessage(EventId = 2, Level = LogLevel.Error, Message = "Error executant {RequestName} després de {ElapsedMilliseconds}ms")]
    partial void LogErrorRequest(string requestName, long elapsedMilliseconds, Exception ex);
}
