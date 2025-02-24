using Provider.Domain.Models;

namespace Provider.Domain.Repositories.Masary
{
    public interface IMasaryRepository
    {
        Task<double> GetServiceChargeAsync(int serviceId, double amount);
        Task<List<MasaryServiceParameter>> GetServiceParametersAsync(int serviceId);

    }
}
