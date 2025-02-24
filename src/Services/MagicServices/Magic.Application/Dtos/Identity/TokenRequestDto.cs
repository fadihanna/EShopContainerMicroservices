namespace Magic.Application.Dtos.Identity
{
    public record TokenRequestDto(
        string Token,
        string RefreshToken 
    );
}
