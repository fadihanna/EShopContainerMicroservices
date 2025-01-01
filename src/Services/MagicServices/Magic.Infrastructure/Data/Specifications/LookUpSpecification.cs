using Magic.Application.Data;
using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class LookUpSpecification : ILookUpSpecification
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public LookUpSpecification(IApplicationDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public string? GetErrorMessageAsync(int errorCode, string language, CancellationToken cancellationToken)
        {
            var errorCodeLookup = GetInternalErrorCodeLookupAsync(cancellationToken).Result
            .Where(o => o.IsActive && o.ErrorCode.Equals(errorCode))
            .FirstOrDefault();

            if (errorCodeLookup == null) return null;

            return language switch
            {
                Language.Arabic => errorCodeLookup.MessageAR,
                _ => errorCodeLookup.MessageEN 
            };
        }

        public async Task<List<InternalErrorCodeLookup>> GetInternalErrorCodeLookupAsync(CancellationToken cancellationToken)
        {
            var cachedErrorCodes = await _cacheService.GetAsync<List<InternalErrorCodeLookup>>(Constants.ErrorCodesCache, cancellationToken);
            if (cachedErrorCodes != null)
                return cachedErrorCodes;

            var errorCodes = await _dbContext.InternalErrorCodeLookups.AsNoTracking().ToListAsync(cancellationToken);
            await _cacheService.SetAsync(Constants.ErrorCodesCache, errorCodes, TimeSpan.FromHours(12), cancellationToken);

            return errorCodes;
        }
    }
}
