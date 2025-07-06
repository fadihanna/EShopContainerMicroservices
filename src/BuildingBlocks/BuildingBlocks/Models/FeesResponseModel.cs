namespace BuildingBlocks.Models;

public record FeesResponseModel(
    string Status,
    string StatusText,
    string DateTime,
    double Amount,
    double Fees,
    double TotalAmount,
    string ProviderReferenceNumber
);
