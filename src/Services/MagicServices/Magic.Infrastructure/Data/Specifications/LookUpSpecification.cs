using Magic.Domain.Enums;
using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Data.Specifications
{
    public class LookUpSpecification : ILookUpSpecification
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICacheService _cacheService;
        private ILanguageService _languageService;

        public LookUpSpecification(IApplicationDbContext dbContext, ICacheService cacheService, ILanguageService languageService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
            _languageService = languageService;
        }

        public string GetErrorMessage(InternalErrorCode errorCode)
        {
            var errorCodeLookup = GetInternalErrorCodeLookupAsync().Result
            .Where(o => o.IsActive && o.ErrorCode.Equals((int)errorCode))
            .FirstOrDefault();

            if (errorCodeLookup == null) return null;

            string language = _languageService.GetLanguage();
            return language switch
            {
                Language.Arabic => errorCodeLookup.MessageAR,
                _ => errorCodeLookup.MessageEN 
            };
        }

        public async Task<List<InternalErrorCodeLookup>> GetInternalErrorCodeLookupAsync()
        {
            var cachedErrorCodes = await _cacheService.GetAsync<List<InternalErrorCodeLookup>>(Constants.ErrorCodesCache);
            if (cachedErrorCodes != null)
                return cachedErrorCodes;

            var errorCodes = await _dbContext.InternalErrorCodeLookups.AsNoTracking().ToListAsync();
            await _cacheService.SetAsync(Constants.ErrorCodesCache, errorCodes, TimeSpan.FromHours(12));

            return errorCodes;
        }
    }
}
