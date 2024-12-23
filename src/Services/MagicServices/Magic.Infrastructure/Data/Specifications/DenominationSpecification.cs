﻿using Magic.Application.Data;
using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class DenominationSpecification : IDenominationSpecification
    {
        private readonly IApplicationDbContext _dbContext;
        public DenominationSpecification(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Denomination>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Denominations.AsNoTracking().ToListAsync();
        }

        public async Task<Denomination> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Denominations.AsNoTracking().Where(o => o.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
