using Magic.Application.Exceptions;

namespace MagicServices.API.Middlewares
{
    /// <summary>
    /// to use this middleware we have to not configure this "app.UseExceptionHandler(options => { });" in DI
    /// should be removed from the startup
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InquiryResponseException ex)
            {
                await HandleExceptionAsync(context, ex.ErrorCode, ex.ErrorMessage ?? ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ex.Message ?? ex.InnerException?.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            var response = new InquiryResponseDto(
                TransactionId: string.Empty,
                Status: statusCode.ToString(),
                StatusText: message,
                DateTime: DateTime.UtcNow.ToString(),
                DetailsList: new List<Details>(),
                Amount:0,
                Fees:0
            );

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
