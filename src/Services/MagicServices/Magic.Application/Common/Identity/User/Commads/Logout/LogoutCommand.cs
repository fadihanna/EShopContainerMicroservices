using Magic.Domain.Enums;

namespace Magic.Application.Common.Identity.User.Commads.Logout;
public record LogoutCommand(LogoutUserDto Request) : IRequest;
public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IUserSpecification _userSpecification;
    private readonly IRefreshTokenSpecification _refreshTokenSpecification;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(
        IUserSpecification userSpecification,
        IRefreshTokenSpecification refreshTokenSpecification,
        IUnitOfWork unitOfWork)
    {
        _userSpecification = userSpecification;
        _refreshTokenSpecification = refreshTokenSpecification;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenSpecification
            .GetRefreshTokenByToken(request.Request.RefreshToken, cancellationToken)
            ?? throw new ForbiddenAccessException(InternalErrorCode.InvalidRefreshToken);

        if (storedToken.IsRevoked)
            throw new ForbiddenAccessException(InternalErrorCode.InvalidRefreshToken);

        storedToken.IsRevoked = true;
        await _refreshTokenSpecification.UpdateRefreshToken(storedToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _userSpecification.Logout();
    }
}
