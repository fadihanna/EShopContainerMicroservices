namespace BuildingBlocks.Models;

public record InquiryResponseModel(
    string TransactionId,
    string Status,
    string StatusText,
    string DateTime,
    double Amount,
    double Fees,
    List<Details> DetailsList
);

public record Details(
    string Key,
    string Value
);

