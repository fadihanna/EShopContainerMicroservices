namespace BuildingBlocks.Models
{
    public record PaymentRequestModel(
         List<InputParameter> InputParameterList,
         decimal Amount,
         decimal Fees,
         decimal TotalAmount,
         int DenominationId,
         string BillingAccount,
         string ProviderCode,
         string RequestId,
         int quantity,
         int PaymentProviderId,
         string InquiryReferenceNumber // refrenceNumber
    );
}
