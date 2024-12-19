namespace Magic.Domain.Models
{
    public class DenominationInputParameter : Entity<int>
    {
        public string Code { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int? Sort { get; set; }
        public bool? IsRequired { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public byte? ParameterType { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsConfirmRequired { get; set; }
        public int DenominationId { get; set; }
        public Denomination Denomination { get; set; }
    }
}
