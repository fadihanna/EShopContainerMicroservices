using Magic.Domain.Enums;

namespace Magic.Application.Common.Identity.User.Commads.RefreshToken
{
    public record RefreshTokenCommand(TokenRequestDto Request) : IRequest<IdentityResponseDto> { }
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, IdentityResponseDto>
    {
        private readonly IUserSpecification _userSpecification;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenSpecification _refreshTokenSpecification;
        private readonly ILookUpSpecification _lookUpSpecification;
        public RefreshTokenCommandHandler(IUserSpecification userSpecification,
            IJwtTokenService jwtTokenService,
            IUnitOfWork unitOfWork,
            IRefreshTokenSpecification refreshTokenSpecification,
            ILookUpSpecification lookUpSpecification)
        {
            _userSpecification = userSpecification;
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
            _refreshTokenSpecification = refreshTokenSpecification;
            _lookUpSpecification = lookUpSpecification;
        }
        public async Task<IdentityResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var verified = await _jwtTokenService.VerifyToken(request.Request, cancellationToken);
            if (!verified.Success)
                return IdentityResponseDto.Failed(
                        new IdentityError
                        {
                            Code = ((int)verified.ErrorCode).ToString(),
                            Description = _lookUpSpecification.GetErrorMessage(verified.ErrorCode.Value),
                        });

            RefreshTokenDto storedToken = await _refreshTokenSpecification.GetRefreshTokenByToken(request.Request.RefreshToken, cancellationToken);
            if (storedToken == null || storedToken.IsRevoked)
                return IdentityResponseDto.Failed(
                        new IdentityError
                        {
                            Code = ((int)InternalErrorCode.InvalidRefreshToken).ToString(),
                            Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.InvalidRefreshToken),
                        });

            var consumerUser = await _userSpecification.GetUserById(verified.UserId.Value);

            storedToken.IsUsed = true;
            await _refreshTokenSpecification.UpdateRefreshToken(storedToken, cancellationToken);
            (string Token, RefreshTokenDto RefreshToken) generatedTokens = await _jwtTokenService.GenerateToken(consumerUser);
            await _refreshTokenSpecification.InsertRefreshToken(generatedTokens.RefreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            
            return IdentityResponseDto.Success(consumerUser, generatedTokens.Token, generatedTokens.RefreshToken.Token);
        }
    }
}
