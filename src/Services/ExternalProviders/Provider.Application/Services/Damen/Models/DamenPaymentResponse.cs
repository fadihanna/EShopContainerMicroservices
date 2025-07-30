namespace Provider.Application.Services.Damen.Models.Payment
{
    public class Result 
    {
        public Dictionary<string, string> userServiceDataFileds { get; set; }
        public string balance { get; set; }
        public string referance_id { get; set; }
        public string formatedReferance_id { get; set; }
        public string inquire_id { get; set; }
    }

    public class DamenPaymentResponse
    {
        public Result result { get; set; }
        public Status status { get; set; }
    }

   
}
