namespace Provider.Application.Services.Masary.Models;

public record MasaryPaymentRequest(
    string? login,
    string? password,
    string? terminal_id,
    string? action,
    int? version,
    string? language,
    MasaryPaymentRequestData? MasaryPaymentRequestData,
    string? requestType
);


public record MasaryPaymentRequestData
(
     List<InputParameterList> InputParameterList,
     int service_version,
     string account_number,
     int service_id,
     string external_id,
     decimal amount,
     decimal service_charge,
     decimal total_amount,
     string inquiry_transaction_id
);
public record InputParameterList
(
    string Key,
    string Value
);
