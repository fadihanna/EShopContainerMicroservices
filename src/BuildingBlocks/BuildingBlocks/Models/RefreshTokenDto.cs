namespace BuildingBlocks.Models
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public int? ConsumerUserId { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
