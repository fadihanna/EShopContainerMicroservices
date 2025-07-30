using System.ComponentModel.DataAnnotations;

namespace Provider.Domain.Models
{
    public class DamenAmount
    {
        [Key]
        public int Id { get; set; }

        public string ArLabel { get; set; }
        public string EnLabel { get; set; }
        public string FieldName { get; set; }

        public string ValueType { get; set; } // e.g., "range"
        public decimal? RangeMin { get; set; }
        public decimal? RangeMax { get; set; }
        public bool IsUserEntered { get; set; }
        public bool CalculateFee { get; set; }
        public bool Credit { get; set; }

        public string Replace { get; set; } // comma-separated names like: "range_max,total_payable_amount"

        public int DamenServiceId { get; set; }
        public DamenService DamenService { get; set; }
    }

}
