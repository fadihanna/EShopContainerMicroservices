using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BuildingBlocks.Models;
using Provider.Application.DTOs;

namespace Provider.Application.Common.Helpers
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
                    {
                        IsSuccess = true,
                        Status = new Status
                        {
                            StatusCode = "204",
                            StatusText = "Timeout: Transaction is pending and will be retried."
                        },
                        PaymentResponseData = new PaymentResponseData
                        {
                            Amount = providerRequest.Amount,
                            Fees = providerRequest.Fees,
                            TotalAmount = providerRequest.Amount + providerRequest.Fees,
                            RequestID = providerRequest.RequestId,
                            TransactionId = providerRequest.RequestId
                        }
                    };
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
