namespace Magic.Domain.Models
{
    public class PaymentProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public static PaymentProvider Create(string name, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            return new PaymentProvider
            {
                Name = name,
                IsActive = isActive,
            };
        }
    }
}