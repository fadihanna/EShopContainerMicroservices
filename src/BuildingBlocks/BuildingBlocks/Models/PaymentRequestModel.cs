namespace BuildingBlocks.Models
{

    public record PaymentRequestModel(
     List<InputParameter> InputParameterList,
     int DenominationId,
     string BillingAccount,
     string RequestId,
     int ProviderId,
     string BillerCode,
        double Fees,
        double Amount,
        string InquiryReferenceNumber
 );
}
