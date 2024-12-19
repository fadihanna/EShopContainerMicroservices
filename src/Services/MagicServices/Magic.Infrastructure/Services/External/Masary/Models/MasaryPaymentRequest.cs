using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Infrastructure.Services.External.Masary.Models
{
    public class MasaryPaymentRequest
    {
        public string login { get; set; }
        public string password { get; set; }
        public string terminal_id { get; set; }
        public string action { get; set; }
        public int version { get; set; }
        public string language { get; set; }
        public MasaryPaymentRequestData MasaryPaymentRequestData { get; set; }
    }


    public class MasaryPaymentRequestData
    {
        public List<InputParameterList> InputParameterList { get; set; }
        public int service_version { get; set; }
        public string account_number { get; set; }
        public int service_id { get; set; }
        public string external_id { get; set; }
        public double amount { get; set; }
        public double service_charge { get; set; }
        public double total_amount { get; set; }
        public string inquiry_transaction_id { get; set; }
    }

    public class InputParameterList
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
