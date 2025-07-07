namespace Magic.Application.Extensions
{
    public static class FeesServiceExtension
    {
        public static FeesRequestModel ToStandardRequest(this FeesRequestDto feesRequestDto, string providerCode)
        {
            return new FeesRequestModel(
                RequestId: feesRequestDto.RequestId,
                Amount: feesRequestDto.Amount,
                ProviderCode: providerCode,
                ProviderId: feesRequestDto.ProviderId
            );
        }
        public static FeesResponseDto ToModelResponse(this FeesResponseModel feesResponseModel)
        {
            return new FeesResponseDto
            (
                ProviderReferenceNumber: feesResponseModel.ProviderReferenceNumber,
                Status: feesResponseModel.Status,
                StatusText: feesResponseModel.StatusText,
                DateTime: feesResponseModel.DateTime,
                Amount: feesResponseModel.Amount,
                Fees: feesResponseModel.Fees,
                TotalAmount: feesResponseModel.TotalAmount
            );
        }
    }
}
