namespace Magic.Infrastructure.Data.Identity.Entity;
public class RefreshToken
{
    public int Id { get; set; }
    public int ConsumerUserId { get; set; }
    public string? Token { get; set; }
    public string? JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }

    public ConsumerUser ConsumerUser { get; set; }
}
