namespace BuildingBlocks.Models;

public record InquiryResponseModel(
    string TransactionId,
    string Status,
    string StatusText,
    string DateTime,
    List<Details> DetailsList
);

public record Details(
    string Key,
    string Value
);

