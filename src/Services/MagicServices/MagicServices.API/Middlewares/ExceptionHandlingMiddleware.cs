using Magic.Application.Exceptions;
using Magic.Domain.Enums;
using Magic.Domain.Specifications;
using Microsoft.AspNetCore.Identity;

namespace MagicServices.API.Middlewares
{
    /// <summary>
    /// to use this middleware we have to not configure this "app.UseExceptionHandler(options => { });" in DI
    /// should be removed from the startup
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        // Make these fields private & set them in each request scope
        private ILookUpSpecification _lookUpSpecification;

        public ExceptionHandlingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Create a service scope for each request
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _lookUpSpecification = scope.ServiceProvider.GetRequiredService<ILookUpSpecification>();

                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await (ex switch
                    {
                        InquiryResponseException inquiryEx => HandleInquiryExceptionAsync(context, inquiryEx.ErrorCode, inquiryEx),
                        ForbiddenAccessException forbiddenEx => HandleForbiddenExceptionAsync(context, forbiddenEx.ErrorCode, forbiddenEx),
                        _ => HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ex.Message ?? ex.InnerException?.Message)
                    });
                }
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
        {
            var response = new InquiryResponseDto(
                TransactionId: string.Empty,
                Status: statusCode.ToString(),
                StatusText: message,
                Amount:0,
                Fees:0,
                DateTime: DateTime.UtcNow.ToString(),
                DetailsList: new List<Details>()
            );

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }
        private async Task HandleInquiryExceptionAsync(HttpContext context, InternalErrorCode statusCode, InquiryResponseException ex)
        {
            string errorMessage = _lookUpSpecification.GetErrorMessage(statusCode) ?? ex.Message;

            var response = new InquiryResponseDto(
                TransactionId: string.Empty,
                Status: statusCode.ToString(),
                StatusText: errorMessage,
                Amount: 0,
                Fees: 0,
                DateTime: DateTime.UtcNow.ToString(),
                DetailsList: new List<Details>()
            );

            context.Response.StatusCode = ((int)statusCode);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandleForbiddenExceptionAsync(HttpContext context, InternalErrorCode statusCode, ForbiddenAccessException ex)
        {
            string errorMessage = _lookUpSpecification.GetErrorMessage(statusCode) ?? ex.Message;

            var response = IdentityResult.Failed(
                new IdentityError() { Code = ((int)statusCode).ToString(), Description = errorMessage }
            );

            context.Response.StatusCode = ((int)statusCode);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
