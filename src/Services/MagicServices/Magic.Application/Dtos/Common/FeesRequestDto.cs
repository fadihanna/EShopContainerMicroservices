
public record FeesRequestDto(
        double Amount,
        int RequestId,
        int DenominationId,
        string ProviderCode,
        int ProviderId
    );