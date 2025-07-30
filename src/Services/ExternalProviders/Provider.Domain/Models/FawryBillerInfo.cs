using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class FawryBillerInfo
    {
        public int Id { get; set; }

        public string BillTypeCode { get; set; }
        public string Name { get; set; }
        public string PmtType { get; set; }
        public bool SupportPartialPay { get; set; }
        public bool SupportOverPay { get; set; }
        public bool SupportInquiry { get; set; }
        public bool SupportReversal { get; set; }

        public int BillerId { get; set; }
        public FawryBiller Biller { get; set; }

        public ICollection<FawryServiceCharge> Fees { get; set; }
        public ICollection<FawryPaymentItem> PaymentItems { get; set; }
    }

}
