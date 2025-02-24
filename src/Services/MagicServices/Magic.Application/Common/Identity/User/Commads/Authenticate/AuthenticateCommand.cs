using Magic.Domain.Enums;

namespace Magic.Application.Common.Identity.User.Commads.Authenticate
{
    public record AuthenticateCommand(LoginUserDto Request) : IRequest<IdentityResponseDto> { }
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, IdentityResponseDto>
    {
        private readonly IUserSpecification _userSpecification;
        private readonly IRefreshTokenSpecification _refreshTokenSpecification;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILookUpSpecification _lookUpSpecification;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticateCommandHandler(IUserSpecification userSpecification,
            IRefreshTokenSpecification refreshTokenSpecification,
            IJwtTokenService jwtTokenService,
            ILookUpSpecification lookUpSpecification, IUnitOfWork unitOfWork)
        {
            _userSpecification = userSpecification;
            _refreshTokenSpecification = refreshTokenSpecification;
            _jwtTokenService = jwtTokenService;
            _lookUpSpecification = lookUpSpecification;
            _unitOfWork = unitOfWork;
        }
        public async Task<IdentityResponseDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            ConsumerUserDto signinUser = await _userSpecification.GetUserByMobile(request.Request.Mobile);
            if (signinUser == null)
                return IdentityResponseDto.Failed(new IdentityError
                {
                    Code = ((int)InternalErrorCode.UserNotExist).ToString(),
                    Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.UserNotExist)
                });

            SignInResult signInResult = await _userSpecification.CheckPasswordSignInAsync(signinUser, request.Request.Password);
            if (signInResult.Succeeded)
            {
                (string Token, RefreshTokenDto RefreshToken) generatedTokens = await _jwtTokenService.GenerateToken(signinUser);
                await _refreshTokenSpecification.DeleteRefreshToken(signinUser.Id, cancellationToken);
                await _refreshTokenSpecification.InsertRefreshToken(generatedTokens.RefreshToken, cancellationToken);
                await _unitOfWork.SaveChangesAsync();
                
                //return a token
                return IdentityResponseDto.Success(signinUser, generatedTokens.Token, generatedTokens.RefreshToken.Token);
            }
            else
                return IdentityResponseDto.Failed(new IdentityError
                {
                    Code = ((int)InternalErrorCode.WrongPassword).ToString(),
                    Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.WrongPassword)
                });
        }
    }
}
