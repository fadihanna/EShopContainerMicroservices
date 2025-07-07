namespace Provider.Application.Services.Masary.Models;

public class MasaryPaymentResponse
{
    public bool success { get; set; }
    public string language { get; set; }
    public string action { get; set; }
    public int version { get; set; }
    public PaymentResponseData data { get; set; }
}

public class PaymentResponseData
{
    public string transaction_id { get; set; }
    public string status { get; set; }
    public string status_text { get; set; }
    public string date_time { get; set; }
    public List<List<PaymentResponseDetails>> details_list { get; set; }

}
public class PaymentResponseDetails
{
    public string key { get; set; }
    public string value { get; set; }
}