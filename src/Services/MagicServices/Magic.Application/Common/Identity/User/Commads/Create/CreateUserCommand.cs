using MapsterMapper;

namespace Magic.Application.Common.Identity.User.Commads.Create
{
    public record CreateUserCommand(RegisterUserDto Request) : IRequest<IdentityResponseDto> { }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResponseDto>
    {
        private readonly IUserSpecification _userSpecification;
        private readonly IRefreshTokenSpecification _refreshTokenSpecification;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(IUserSpecification userSpecification, 
            IRefreshTokenSpecification refreshTokenSpecification, 
            IJwtTokenService jwtTokenService, 
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userSpecification = userSpecification;
            _refreshTokenSpecification = refreshTokenSpecification;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IdentityResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            IdentityResponseDto createResult = await _userSpecification.InsertUserAsync(_mapper.Map<ConsumerUserDto>(request.Request));
            if (createResult.Succeeded)
            {
                IdentityResult addToRoleResult = await _userSpecification.AddToRoleAsync(createResult.ConsumerUser, Enums.RolesEnum.User);
                //TODO: two factor auth "otp"

                (string Token, RefreshTokenDto RefreshToken) generatedTokens = await _jwtTokenService.GenerateToken(createResult.ConsumerUser);
                await _refreshTokenSpecification.InsertRefreshToken(generatedTokens.RefreshToken, cancellationToken);
                await _unitOfWork.SaveChangesAsync();
                
                //return a token
                createResult.Token = generatedTokens.Token;
                createResult.RefreshToken = generatedTokens.RefreshToken.Token;
                return createResult;
            }
            else
                return createResult;
        }
    }
}
