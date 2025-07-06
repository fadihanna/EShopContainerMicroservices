public record FeesResponseDto(
        double Fees,
        double Amount,
        double TotalAmount,
        string Status,
        string StatusText,
        string DateTime,
        string ProviderReferenceNumber
    );