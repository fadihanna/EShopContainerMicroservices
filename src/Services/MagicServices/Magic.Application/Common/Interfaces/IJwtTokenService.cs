namespace Magic.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        Task<(string Token, RefreshTokenDto RefreshToken)> GenerateToken(ConsumerUserDto user);
        Task<RefreshTokenResponseDto> VerifyToken(TokenRequestDto tokenRequest, CancellationToken cancellationToken);
    }
}
