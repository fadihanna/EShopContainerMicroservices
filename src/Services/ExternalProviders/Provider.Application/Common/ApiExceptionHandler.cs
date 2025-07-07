using BuildingBlocks.Models;
using Serilog;
using static System.TimeZoneInfo;

namespace Provider.Application.Common
{
    public class ApiExceptionHandler
    {
        public async Task<T> HandleApiExceptionsAsync<T>(
            Func<Task<T>> apiCall,
            string methodName,
            PaymentRequestModel? providerRequest = null)
        {
            try
            {
                return await apiCall();
            }
            catch (TimeoutException ex)
            {
                Log.Error(ex, "Masary API request timed out in {Method}.", methodName);

                if (providerRequest != null)
                {
                    return (T)(object)new PaymentResponseModel
                    (
                        IsSuccess: true,
                        Status: "204",
                        StatusText: "Timeout: Transaction is pending and will be retried.",
                         TransactionTime: DateTime.Now.ToString(),
                         TransactionId: 0,
                         ProviderTransactionId: "0",
                         UserId: "1",
                         Amount: "0",
                         Fees: "0",
                         TotalAmount: "0",
                         BillingAccount: string.Empty,new List<ResponseDetail>()
                    );
                }

                throw;
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "HTTP Request error in {Method}: {Message}", methodName, ex.Message);
                throw new Exception("Service unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error in {Method}: {Message}", methodName, ex.Message);
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}
