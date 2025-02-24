using Magic.Domain.Enums;

namespace Magic.Application.Dtos.Identity;
public record RefreshTokenResponseDto
(
    int? UserId,
    bool Success,
    InternalErrorCode? ErrorCode
);
