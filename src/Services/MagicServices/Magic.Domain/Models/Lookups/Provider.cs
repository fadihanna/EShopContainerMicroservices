namespace Magic.Domain.Models.Lookups
{
    public class Provider : LookUpBase<int>
    {
        public ICollection<DenominationProviderCode> DenominationProviderCodes { get; set; }
    }
}
