using Serilog;
using System.Diagnostics;

namespace Provider.Application.Logging
{
    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler() { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            string requestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : string.Empty;

            Log.Information("Sending HTTP {Method} request to {Url}. Request Body: {RequestBody}",
                request.Method, request.RequestUri, requestBody);

            HttpResponseMessage response;
            try
            {
                response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                string responseBody = await response.Content.ReadAsStringAsync();

                Log.Information("Received HTTP {StatusCode} response from {Url} in {ElapsedMs}ms. Response Body: {ResponseBody}",
                    response.StatusCode, request.RequestUri, stopwatch.ElapsedMilliseconds, responseBody);

                return response;
            }
            catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                stopwatch.Stop();
                Log.Error(ex, "Request to {Url} timed out after {ElapsedMs}ms.", request.RequestUri, stopwatch.ElapsedMilliseconds);
                throw;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Log.Error(ex, "Error occurred while calling {Url} after {ElapsedMs}ms.", request.RequestUri, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
