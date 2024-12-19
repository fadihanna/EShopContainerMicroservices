namespace Magic.Application.Dtos.Common;
public record InquiryResponseDto(
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
