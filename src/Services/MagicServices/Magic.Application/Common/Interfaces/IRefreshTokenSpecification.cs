namespace Magic.Application.Common.Interfaces
{
    public interface IRefreshTokenSpecification
    {
        Task InsertRefreshToken(RefreshTokenDto refreshToken, CancellationToken cancellationToken);
        Task UpdateRefreshToken(RefreshTokenDto refreshToken, CancellationToken cancellationToken);
        Task DeleteRefreshToken(int userId, CancellationToken cancellationToken);
        Task<RefreshTokenDto> GetRefreshTokenByToken(string token, CancellationToken cancellationToken);
        Task<bool> IsValidTokenAsync(string jwtId, int userId);
    }
}
