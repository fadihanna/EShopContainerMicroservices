using static MassTransit.ValidationResultExtensions;

namespace Provider.Application.Services.Damen.Models
{
    public class DamenInquiryResponse
    {
        public Result result { get; set; }
        public Status status { get; set; }
    }
    public class Status
    {
        public string level { get; set; }
        public string code { get; set; }
        public Msg msg { get; set; }
    }
 
    public class Msg
    {
        public string UserMessage { get; set; }
        public string UserMessageAr { get; set; }
    }

    public class Result
    {
        public Dictionary<string, string> userServiceDataFileds { get; set; }
        public string inquire_id { get; set; }
    }

}
