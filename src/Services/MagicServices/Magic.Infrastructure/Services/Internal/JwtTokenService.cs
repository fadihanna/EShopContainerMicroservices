using BuildingBlocks.Models;
using Magic.Application.Dtos.Identity;
using Magic.Infrastructure.Data.Identity.Entity;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic.Infrastructure.Services.Internal
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly AppSettings _config;
        private readonly IRefreshTokenSpecification _refreshTokenSpecification;
        private readonly IMapper _mapper;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public JwtTokenService(IOptions<AppSettings> config,
            IRefreshTokenSpecification refreshTokenSpecification,
            TokenValidationParameters tokenValidationParameters, 
            IMapper mapper)
        {
            _config = config.Value;
            _refreshTokenSpecification = refreshTokenSpecification;
            _tokenValidationParameters = tokenValidationParameters;
            _mapper = mapper;
        }

        public async Task<(string Token, RefreshTokenDto RefreshToken)> GenerateToken(ConsumerUserDto user)
        {
            JwtSecurityTokenHandler? jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Convert.ToBase64String(Encoding.UTF8.GetBytes(_config.IdentityConfig.JwtSettings.SigningKey));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Mobile),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(_config.IdentityConfig.JwtSettings.RefreshTokenExpirationMinutes),
                Issuer = _config.IdentityConfig.JwtSettings.ValidIssuer,
                Audience = _config.IdentityConfig.JwtSettings.ValidAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(key)), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token
            SecurityToken? token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = jwtTokenHandler.WriteToken(token);

            // Create refresh token
            RefreshToken refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                ConsumerUserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMonths(1),
                Token = CommonMethods.GetRandomString() + Guid.NewGuid()
            };
            return (Token: jwtToken, RefreshToken: _mapper.Map<RefreshTokenDto>(refreshToken));
        }

        public async Task<RefreshTokenResponseDto> VerifyToken(TokenRequestDto tokenRequest, CancellationToken cancellationToken)
        {
            JwtSecurityTokenHandler? jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                RefreshTokenDto? storedToken = await _refreshTokenSpecification.GetRefreshTokenByToken(tokenRequest.RefreshToken, cancellationToken);
                if (storedToken == null)
                    return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.TokenNotFound);

                ClaimsPrincipal? tokenVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

                var jti = tokenVerification.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedToken.JwtId != jti)
                    return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.InvalidRefreshToken);

                long utcExpireDate = long.Parse(tokenVerification.Claims.FirstOrDefault(d => d.Type == JwtRegisteredClaimNames.Exp).Value);

                // UTC to DateTime
                DateTime expireDate = CommonMethods.UTCtoDateTime(utcExpireDate);

                if (expireDate <= DateTime.UtcNow)
                    return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.TokenExpired);


                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    bool result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (!result)
                        return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.GeneralError);
                }

                if (storedToken.IsUsed)
                    return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.TokenUsed);

                if (storedToken.IsRevoked)
                    return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.TokenRevoked);

                // return token
                return new RefreshTokenResponseDto(storedToken.ConsumerUserId, true, null);

            }
            catch (Exception e)
            {
                return new RefreshTokenResponseDto(null, false, DomainEnums.InternalErrorCode.GeneralError);
            }
        }
        
    }
}