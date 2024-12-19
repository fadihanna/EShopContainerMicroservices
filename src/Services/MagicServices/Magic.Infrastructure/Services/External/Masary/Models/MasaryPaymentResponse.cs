namespace Magic.Infrastructure.Services.External.Masary.Models;

public class MasaryPaymentResponse
{
    public bool success { get; set; }
    public string language { get; set; }
    public string action { get; set; }
    public int version { get; set; }
    public int error_code { get; set; }
    public string error_text { get; set; }
    public PaymentResponseDetails PaymentResponseDetails { get; set; }
}
public class PaymentResponseDetails
{

    public string key { get; set; }
    public string value { get; set; }
}