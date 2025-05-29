

namespace Magic.Domain.Specifications
{
    public interface IServiceSpecification : IGenericRepositoryAsync<Service>
    {
        Task<List<Service>> GetAllWithDenominationsAsync(CancellationToken cancellationToken);
    }
}
