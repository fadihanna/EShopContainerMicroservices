namespace Provider.Application.Services.Masary.Models;
public record MasaryInquiryRequest(
     string Lang,
     int ServiceId,
     double Amount,
     int? Quantity,
     string ParameterInput,
     string ExternalId,
     bool IsPayAfterInquire
);