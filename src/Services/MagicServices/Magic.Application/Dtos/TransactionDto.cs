namespace Magic.Application.Dtos
{
    public record TransactionDto(
     string UserId,
     decimal Amount,
     decimal Fees,
     decimal TotalAmount,
     int RequestId,
     int DenominationId,
     int PaymentProviderId,
     int Status,
     string BillingAccount,
     int quantity,
     //List<InputParameter> InputParameterList,
     bool IsRefunded = false
    );
    public record InputParameter(
    string Key,
    string Value
    );
}
