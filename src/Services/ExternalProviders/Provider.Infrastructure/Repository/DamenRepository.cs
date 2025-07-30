using Microsoft.EntityFrameworkCore;
using Provider.Application.Data;
using Provider.Application.Dtos;
using Provider.Application.Repositories.Damen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Provider.Infrastructure.Repository
{
    public class DamenRepository : IDamenRepository
    {
        private readonly IProviderDbContext _context;

        public DamenRepository(IProviderDbContext context)
        {
            _context = context;
        }

        public async Task<double> GetServiceChargeAsync(int serviceId, double amount)
        {
            var service = await _context.DamenServices
                .Include(s => s.ServiceCharge)
                    .ThenInclude(sc => sc.Slides)
                .FirstOrDefaultAsync(s => s.Id == serviceId);

            if (service?.ServiceCharge == null)
                throw new KeyNotFoundException($"Service with ID {serviceId} or its charges not found.");

            var matchingSlide = service.ServiceCharge.Slides
    .FirstOrDefault(s => amount >= (double)s.FromValue && amount <= (double)s.ToValue);

            if (matchingSlide == null)
                throw new InvalidOperationException($"No matching charge range found for amount {amount}.");

            double chargeAmount = matchingSlide.ScValueType == "P"
                ? (amount * (double)matchingSlide.ScValue / 100.0)
                : (double)matchingSlide.ScValue;

            return chargeAmount;
        }


        public async Task<List<DamenServiceRequestDto>> GetServiceRequestsWithInputFieldsAsync(int serviceId)
        {
            var requests = await _context.DamenServiceRequests
                .Where(r => r.DamenServiceId == serviceId)
                .Include(r => r.Inputs)
                    .ThenInclude(i => i.DataField)
                .Select(r => new DamenServiceRequestDto
                {
                    Name = r.Name,
                    RequestType = r.RequestType,
                    Inputs = r.Inputs.Select(i => new DataFieldDto
                    {
                        FieldName = i.DataField.FieldName,
                        InputType = i.DataField.InputType
                    }).ToList()
                })
                .ToListAsync();

            return requests;
        }

    }
}
