
namespace Magic.Domain.Specifications
{
    public interface IServiceSpecification : IGenericRepositoryAsync<Service>
    {
        Task<List<Service>> GetServiceDenominationGroupAsync(int categoryId, CancellationToken cancellationToken);
        Task<List<Service>> GetServiceDenominationAsync(int categoryId, CancellationToken cancellationToken);
    }
}
