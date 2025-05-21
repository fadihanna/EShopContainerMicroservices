namespace BuildingBlocks.Models;
public record FeesRequestModel(
    int DenomiantionId,
    double Amount,
    int ProviderId,
    string BillerCode
);
