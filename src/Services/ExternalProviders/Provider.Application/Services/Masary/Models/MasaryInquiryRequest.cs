namespace Provider.Application.Services.Masary.Models;
public record MasaryInquiryRequest(
string login,
    string password,
    string terminal_id,
    string action,
    int version,
    string language,
    MasaryInquiryRequestData data
);
public record MasaryInquiryRequestData(
    List<MasaryInputParameter> input_parameter_list,
    int service_version,
    string account_number,
    int service_id,
    string external_id
);
public record MasaryInputParameter(
    string Key,
    string Value
);