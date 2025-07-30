using System.ComponentModel.DataAnnotations;

namespace Provider.Domain.Models
{
    public class DamenServiceRequest
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string RequestType { get; set; } // e.g., "I" or "P"
        public int Order { get; set; }

        public int DamenServiceId { get; set; }
        public DamenService DamenService { get; set; }

        public ICollection<DamenServiceRequestInputField> Inputs { get; set; } = new List<DamenServiceRequestInputField>();
        public ICollection<DamenServiceRequestOutputField> Outputs { get; set; } = new List<DamenServiceRequestOutputField>();

    }


}
