namespace Magic.Domain.Models
{
    public class PaymentProvider 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
