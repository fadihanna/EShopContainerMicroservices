namespace Magic.Application.Dtos
{
     public record BalanceDto(
     string  UserId,
     decimal Amount, 
     string  Description,
     string  InvoiceId,
     DateTime CreatedOn, 
     decimal Before,
     decimal After,
     string  BillingAccount,
     decimal CurrentBalance,
     int ProviderId
    );
}
