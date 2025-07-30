using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class FawryServiceChargeTier
    {
        public int Id { get; set; }

        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal Charge { get; set; }
        public bool IsPercentage { get; set; }

        // Navigation
        public int ServiceChargeId { get; set; }
        public FawryServiceCharge ServiceCharge { get; set; }
    }
}
