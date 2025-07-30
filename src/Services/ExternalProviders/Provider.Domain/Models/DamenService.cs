using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Models
{
    public class DamenService
    {
        [Key]
        public int Id { get; set; } // = ser_id
        public string ArName { get; set; }
        public string EnName { get; set; }

        public DamenAmount Amount { get; set; }
        public DamenServiceCharge ServiceCharge { get; set; }

        public ICollection<DamenServiceField> DataFields { get; set; } = new List<DamenServiceField>();
        public ICollection<DamenServiceRequest> Requests { get; set; } = new List<DamenServiceRequest>();

    }

}
