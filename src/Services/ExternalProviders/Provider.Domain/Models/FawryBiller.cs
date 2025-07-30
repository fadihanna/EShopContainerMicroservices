using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class FawryBiller
    {
        public int Id { get; set; }
        public string BillerId { get; set; }
        public string BillerName { get; set; }
        public bool IsSupportPartialPay { get; set; }
        public bool IsSupportOverPay { get; set; }

        public ICollection<FawryBillerInfo> BillerInfos { get; set; }
    }

}
