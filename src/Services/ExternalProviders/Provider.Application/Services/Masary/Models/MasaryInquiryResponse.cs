namespace Provider.Application.Services.Masary.Models;

public class MasaryInquiryResponse
{
    public bool success { get; set; }
    public string transaction_id { get; set; }
    public int ServiceVersion { get; set; }
    public double chargeAmount { get; set; }
    public double amount { get; set; }
    public string parameterInput { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public string InfoText { get; set; } = string.Empty;

    public InquiryResponseDetails InquiryResponseDetails { get; set; }
}
public class InquiryResponseDetails
{
    public string transaction_id { get; set; }
    public string status { get; set; }
    public string status_text { get; set; }
    public string date_time { get; set; }
    public string info_text { get; set; } = string.Empty;
    public double amount { get; set; }
    public double min_amount { get; set; }
    public double max_amount { get; set; }
}
