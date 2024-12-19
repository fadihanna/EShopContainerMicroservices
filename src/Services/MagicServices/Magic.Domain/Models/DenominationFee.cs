namespace Magic.Domain.Models
{
    public class DenominationFee : Entity<int>
    {
        // Properties
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }
        public decimal? Fees { get; set; }
        public decimal? Slap { get; set; }
        public bool? IsPercentage { get; set; }

        public int DenominationId { get; set; }
        public Denomination Denomination { get; set; }

        // Factory Method for Creation
        public static DenominationFee Create(
            decimal amountFrom,
            decimal amountTo,
            decimal fees,
            decimal slap,
            bool isPercentage,
            int denominationId)
        {
            if (amountFrom < 0)
                throw new ArgumentException("AmountFrom cannot be negative.", nameof(amountFrom));

            if (amountTo <= amountFrom)
                throw new ArgumentException("AmountTo must be greater than AmountFrom.", nameof(amountTo));

            if (denominationId <= 0)
                throw new ArgumentException("DenominationId must be a positive number.", nameof(denominationId));

            return new DenominationFee
            {
                AmountFrom = amountFrom,
                AmountTo = amountTo,
                Fees = fees,
                Slap = slap,
                IsPercentage = isPercentage,
                DenominationId = denominationId
            };
        }

        // Method for Updating
        public void Update(
            decimal amountFrom,
            decimal amountTo,
            decimal fees,
            decimal slap,
            bool isPercentage)
        {
            if (amountFrom < 0)
                throw new ArgumentException("AmountFrom cannot be negative.", nameof(amountFrom));

            if (amountTo <= amountFrom)
                throw new ArgumentException("AmountTo must be greater than AmountFrom.", nameof(amountTo));

            AmountFrom = amountFrom;
            AmountTo = amountTo;
            Fees = fees;
            Slap = slap;
            IsPercentage = isPercentage;
        }
    }
}
