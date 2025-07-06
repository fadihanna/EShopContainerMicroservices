namespace Magic.Domain.Models
{
    public class DenominationProviderCode : Entity<int>
    {
        public int DenominationId { get; private set; }
        public int ProviderId { get; private set; }
        public string ProviderCode { get; private set; } = default!;
        public Denomination Denomination { get; private set; } = default!;
        public Provider Provider { get; private set; } = default!;

        public static DenominationProviderCode Create(
            int denominationId,
            int providerId,
            string providerCode)
        {
            if (denominationId <= 0)
                throw new ArgumentException("DenominationId must be a positive number.", nameof(denominationId));

            if (providerId <= 0)
                throw new ArgumentException("ProviderId must be a positive number.", nameof(providerId));

            return new DenominationProviderCode
            {
                DenominationId = denominationId,
                ProviderId = providerId,
                ProviderCode = providerCode
            };
        }

        public void Update(int providerId, string providerCode)
        {
            if (providerId <= 0)
                throw new ArgumentException("ProviderId must be a positive number.", nameof(providerId));

            ProviderId = providerId;
            ProviderCode = ProviderCode;
        }
    }
}
