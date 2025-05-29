namespace Magic.Domain.Specifications
{
    public interface IDenominationGroupSpecification
    {
        Task<IEnumerable<DenominationGroup>> GetAllAsync(CancellationToken cancellationToken);
    }
}
