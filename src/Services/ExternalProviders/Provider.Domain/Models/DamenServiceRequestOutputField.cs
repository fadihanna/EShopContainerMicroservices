namespace Provider.Domain.Models
{
    public class DamenServiceRequestOutputField
    {
        public int DamenServiceRequestId { get; set; }
        public DamenServiceRequest DamenServiceRequest { get; set; }

        public int DataFieldId { get; set; }
        public DamenServiceField DataField { get; set; }
    }

}
