using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Dtos
{
    public  class ServiceDenominationDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SortOrder { get; set; }
        public int ServiceCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string IconName { get; set; }
        public List<DenominationFullDto> Denominations { get; set; }
    }
    public class DenominationFullDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int SortOrder { get; set; }
        public bool IsInquiryRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsPartial { get; set; }
        public string? Value { get; set; }
        public List<InputParameterDto> InputParameterList { get; set; }
    }

    public class InputParameterDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Placeholder { get; set; }
        public string Type { get; set; }
    }
}
