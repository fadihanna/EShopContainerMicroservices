using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class DamenServiceCharge
    {
        [Key]
        public int Id { get; set; }

        public string ArLabel { get; set; }
        public string EnLabel { get; set; }
        public string FieldName { get; set; }

        public int DamenServiceId { get; set; }
        public DamenService DamenService { get; set; }

        public ICollection<DamenServiceChargeSlide> Slides { get; set; } = new List<DamenServiceChargeSlide>();

    }

}
