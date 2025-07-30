using System.ComponentModel.DataAnnotations;

namespace Provider.Domain.Models
{
    public class DamenServiceChargeSlide
    {
        [Key]
        public int Id { get; set; }

        public string ScValueType { get; set; } // e.g., "F" or "P"
        public decimal FromValue { get; set; }
        public decimal ToValue { get; set; }
        public decimal ScValue { get; set; }

        public int DamenServiceChargeId { get; set; }
        public DamenServiceCharge DamenServiceCharge { get; set; }
    }

}
