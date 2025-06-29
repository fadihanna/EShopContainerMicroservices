using System.Collections.Generic;

namespace BeePaymentDTO.Consumer
{
    public class ServiceWithDenominationDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SortOrder { get; set; }
        public int ServiceCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string IconName { get; set; }
        public List<InputParameter> InputParameterList { get; set; }
        public List<DenominationGroup> DenominationGroup { get; set; }
    }

    public class InputParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string PlaceHolder { get; set; }
        public string Type { get; set; }
    }
    public class DenominationGroup
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SortOrder { get; set; }
        public bool IsInquiryRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsPartial { get; set; }
        public List<Denomination> Denominations { get; set; }
    }
    public class Denomination
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
