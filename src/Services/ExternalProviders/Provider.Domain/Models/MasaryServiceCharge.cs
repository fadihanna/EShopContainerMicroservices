using System.ComponentModel.DataAnnotations.Schema;

namespace Provider.Domain.Models
{
    public class MasaryServiceCharge
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public double From { get; set; }
        public double To { get; set; }
        public double Charge { get; set; }
        public double Slap { get; set; }
        public bool Percentage { get; set; }
        [ForeignKey("ServiceId")]
        public MasaryService MasaryServiceslist { get; set; }
    }
}
