﻿namespace Provider.Application.Services.Masary.Models;

public class MasaryInquiryResponse
{
    public bool success { get; set; }
    public string language { get; set; }
    public string action { get; set; }
    public int version { get; set; }
    public InquiryResponseDetails data { get; set; }
}
public class InquiryResponseDetails
{
    public string transaction_id { get; set; }
    public int status { get; set; }
    public string status_text { get; set; }
    public string date_time { get; set; }
    public string info_text { get; set; }
    public double amount { get; set; }
    public double min_amount { get; set; }
    public double max_amount { get; set; }
}
