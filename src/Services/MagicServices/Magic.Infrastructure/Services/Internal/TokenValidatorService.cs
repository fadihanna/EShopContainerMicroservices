using Magic.Domain.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Magic.Infrastructure.Services.Internal
{
    public class TokenValidatorService(TokenValidationParameters _tokenValidationParameters,
        IUserSpecification _userSpecification,
        IRefreshTokenSpecification _refreshTokenSpecification, ILookUpSpecification _lookUpSpecification) : ITokenValidatorService
    {
        public async Task ValidateAsync(TokenValidatedContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail(_lookUpSpecification.GetErrorMessage(DomainEnums.InternalErrorCode.InvalidTokenNoClaims));
                return;
            }

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdString, NumberStyles.Number, CultureInfo.InvariantCulture, out var userId))
            {
                context.Fail(_lookUpSpecification.GetErrorMessage(DomainEnums.InternalErrorCode.InvalidTokenNoUserId));
                return;
            }

            var user = await _userSpecification.GetUserById(userId);
            if (user == null)
            {
                context.Fail(_lookUpSpecification.GetErrorMessage(DomainEnums.InternalErrorCode.TokenExpired));
                return;
            }

            if (context.SecurityToken is not Microsoft.IdentityModel.JsonWebTokens.JsonWebToken jwtToken)
            {
                context.Fail(_lookUpSpecification.GetErrorMessage(DomainEnums.InternalErrorCode.InvalidTokenNoClaims));
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(jwtToken.UnsafeToString(), _tokenValidationParameters, out var validatedToken);
                var jti = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

                if (string.IsNullOrWhiteSpace(jti) || !await _refreshTokenSpecification.IsValidTokenAsync(jti, userId))
                {
                    context.Fail(_lookUpSpecification.GetErrorMessage(DomainEnums.InternalErrorCode.InvalidTokenNotExist));
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Fail(_lookUpSpecification.GetErrorMessage(DomainEnums.InternalErrorCode.TokenValidationFailed) + ex.Message);
                return;
            }
        }
    }
}
