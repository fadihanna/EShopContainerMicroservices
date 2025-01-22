using BuildingBlocks.Interfaces;
using MediatR;
using Serilog;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;
public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger _logger;
    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Log request details
        if (request is ITransactionRequest denominationRequest)
            _logger.Information("Processing request: {RequestType} with DenominationId: {DenominationId}, ExternalId: {ExternalId}",
                            typeof(TRequest).Name, denominationRequest.DenominationId);
        else
            return await next();

        var timer = Stopwatch.StartNew(); // Start the timer

        TResponse response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;

        // Log warnings if execution time exceeds threshold
        if (timeTaken.TotalSeconds > 3)
            _logger.Warning("[PERFORMANCE] {RequestName} took {ElapsedTime} seconds to execute.",
                typeof(TRequest).Name, timeTaken.TotalSeconds);

        // Log response details
        _logger.Information("[END] Request: {RequestName} - Response: {@Response}",
            typeof(TRequest).Name, response);

        return response;
    }
}
