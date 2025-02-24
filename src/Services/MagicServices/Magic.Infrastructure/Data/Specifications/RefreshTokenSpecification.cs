using BuildingBlocks.Models;
using Magic.Infrastructure.Data.Identity.Entity;
using MapsterMapper;

namespace Magic.Infrastructure.Data.Specifications
{
    public class RefreshTokenSpecification : IRefreshTokenSpecification
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<RefreshToken> _refreshTokens;
        public RefreshTokenSpecification(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTokens = _unitOfWork.Set<RefreshToken>();
        }

        public async Task InsertRefreshToken(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            await _refreshTokens.AddAsync(refreshToken);
        }

        public async Task<RefreshTokenDto> GetRefreshTokenByToken(string token, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = await _refreshTokens.AsNoTracking().FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
            return _mapper.Map<RefreshTokenDto>(refreshToken);
        }

        public async Task UpdateRefreshToken(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            _refreshTokens.Attach(refreshToken);
            _refreshTokens.Entry(refreshToken).State = EntityState.Modified;
        }
        public async Task DeleteRefreshToken(int userId, CancellationToken cancellationToken)
        {
            //await _refreshTokens.Where(x => x.ConsumerUserId == userId && !x.IsUsed && !x.IsRevoked).ExecuteDeleteAsync();
            await _refreshTokens.Where(x => x.ConsumerUserId == userId && (!x.IsUsed || x.IsRevoked)).ExecuteDeleteAsync();
        }
        public async Task<bool> IsValidTokenAsync(string jwtId, int userId)
        {
            var storedToken = await _refreshTokens.FirstOrDefaultAsync(x => x.JwtId == jwtId && x.ConsumerUserId == userId);
            return storedToken != null && !storedToken.IsRevoked && !storedToken.IsUsed;
        }
    }
}
