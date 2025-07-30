using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class FawryPaymentItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public int BillerInfoId { get; set; }
        public FawryBillerInfo BillerInfo { get; set; }
    }

}
