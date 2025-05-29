using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Dtos
{
    public class ServiceWithDenominationDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; } = string.Empty;
        public string NameAR { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public int ServiceCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string IconName { get; set; } = string.Empty;
        public List<DenominationGroupDto> DenominationGroup { get; set; } = new();
    }

    public class DenominationGroupDto
    {
        public int Id { get; set; }
        public string NameEN { get; set; } = string.Empty;
        public string NameAR { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public bool IsInquiryRequired { get; set; }
        public bool IsActive { get; set; }
        public List<DenominationItemDto> Denominations { get; set; } = new();
    }

    public class DenominationItemDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
