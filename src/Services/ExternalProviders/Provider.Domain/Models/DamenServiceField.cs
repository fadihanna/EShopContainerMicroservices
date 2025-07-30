using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class DamenServiceField
    {
        [Key]
        public int Id { get; set; }

        public string FieldName { get; set; }
        public string ArLabel { get; set; }
        public string EnLabel { get; set; }
        public string InputType { get; set; }

        public int? MaxLen { get; set; }
        public int? MinLen { get; set; }
        public string DefaultVal { get; set; }
        public bool? Visible { get; set; }
        public bool? Required { get; set; }
        public bool? IsPassword { get; set; }
        public bool? Confirmed { get; set; }

        public int DamenServiceId { get; set; }
        public DamenService DamenService { get; set; }
    }

}
