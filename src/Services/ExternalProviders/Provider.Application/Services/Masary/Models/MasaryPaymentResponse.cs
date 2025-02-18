namespace Provider.Application.Services.Masary.Models;

public class MasaryPaymentResponse
{
    public bool success { get; set; }
    public string language { get; set; }
    public string action { get; set; }
    public int version { get; set; }
    public int error_code { get; set; }
    public string error_text { get; set; }
    public PaymentResponseData PaymentResponseData { get; set; }
}

public class PaymentResponseData
{
    public string transaction_id { get; set; }
    public string status { get; set; }
    public string status_text { get; set; }
    public string date_time { get; set; }
    public string response_code { get; set; }
    public List<PaymentResponseDetails> paymentResponseDetails { get; set; }

}
public class PaymentResponseDetails
{
    public string key { get; set; }
    public string value { get; set; }
}