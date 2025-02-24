using System.ComponentModel.DataAnnotations.Schema;

namespace Provider.Domain.Models
{
    public class MasaryServiceParameter
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int Position { get; set; }
        public bool Visible { get; set; }
        public bool Required { get; set; }
        public string ParameterType { get; set; }
        public bool ClientId { get; set; }
        public string DefaultValue { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool ConfirmRequired { get; set; }
        [ForeignKey("ServiceId")]
        public MasaryService MasaryServiceslist { get; set; }
    }
}
