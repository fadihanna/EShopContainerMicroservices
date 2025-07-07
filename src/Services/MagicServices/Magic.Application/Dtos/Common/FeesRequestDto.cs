
public record FeesRequestDto(
        double Amount,
        int RequestId,
        int DenominationId,
        int ProviderId
    );