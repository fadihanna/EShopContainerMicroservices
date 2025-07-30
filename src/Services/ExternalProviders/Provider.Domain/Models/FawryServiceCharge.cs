using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class FawryServiceCharge
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public decimal Value { get; set; }

        public int BillerInfoId { get; set; }
        public FawryBillerInfo BillerInfo { get; set; }
        public ICollection<FawryServiceChargeTier> FeesTiers { get; set; }

    }
}
