
namespace Magic.Application.Dtos.Identity
{
    public class IdentityResponseDto : IdentityResult
    {
        public ConsumerUserDto ConsumerUser { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public new IEnumerable<IdentityError> Errors { get; private set; } = Enumerable.Empty<IdentityError>();

        // Static method to return a failed response
        public static IdentityResponseDto Failed(params IdentityError[] errors)
        {
            return new IdentityResponseDto
            {
                Succeeded = false,
                Errors = errors
            };
        }
        public static IdentityResponseDto Success(ConsumerUserDto consumerUser, string? token = null, string? refreshToken = null)
        {
            return new IdentityResponseDto
            {
                Succeeded = true,
                ConsumerUser = consumerUser,
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}
