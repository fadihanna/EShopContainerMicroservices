namespace BuildingBlocks.Models;
public record FeesRequestModel(
    double Amount,
    int RequestId,
    int ProviderId,
    string ProviderCode
);
