using Microsoft.EntityFrameworkCore;
using Provider.Application.Data;
using Provider.Domain.Models;
using Provider.Domain.Repositories.Masary;

namespace Provider.Infrastructure.Repository.Masary
{
    public class MasaryRepository : IMasaryRepository
    {
        private readonly IProviderDbContext _context;

        public MasaryRepository(IProviderDbContext context)
        {
            _context = context;
        }
        public async Task<double> GetServiceChargeAsync(int serviceId, double amount)
        {
            var service = await _context.MasaryService
                .Include(s => s.ServiceCharges)
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId);

            if (service == null)
            {
                throw new KeyNotFoundException("Service not found.");
            }

            var charge = service.ServiceCharges
               .Where(c => amount >= c.From && amount <= c.To)
               .Select(c => c.Percentage ? (amount * c.Charge / 100) : c.Charge)
               .FirstOrDefault();

            return charge;
        }
        public async Task<List<MasaryServiceParameter>> GetServiceParametersAsync(int serviceId)
        {
            var service = await _context.MasaryService
                .Include(s => s.ServiceParameter)
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId);

                if (service == null)
                {
                    throw new KeyNotFoundException("Service not found.");
                }

            return service.ServiceParameter
               .Select(p => new MasaryServiceParameter
               {
                   Name = p.Name,
                   ParameterType = p.ParameterType
               })
               .ToList();
        }
    }
}
