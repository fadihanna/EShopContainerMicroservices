namespace Magic.Domain.Models
{
    public class DenominationAmount : Entity<int>
    {
        public decimal Value { get;  set; }
        public int DenominationId { get; private set; } // Foreign Key
        public Denomination Denomination { get; private set; } = default!;

        public static DenominationAmount Create(decimal value, int denominationId)
        {
            return new DenominationAmount
            {
                Value = value,
                DenominationId = denominationId
            };
        }
    }
}
