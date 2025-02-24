using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Magic.Infrastructure.Services.Internal
{
    public interface ITokenValidatorService
    {
        Task ValidateAsync(TokenValidatedContext context);
    }
}
