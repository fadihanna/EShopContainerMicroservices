using Magic.Application.Common.Enums;
using Magic.Application.Dtos.Identity;
using Magic.Domain.Specifications;
using Magic.Infrastructure.Data.Identity.Entity;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Magic.Infrastructure.Data.Specifications
{
    public class UserSpecification : BaseSpecification, IUserSpecification
    {
        private readonly UserManager<ConsumerUser> _userManager;
        private readonly SignInManager<ConsumerUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ConsumerUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        public UserSpecification(UserManager<ConsumerUser> userManager,
            SignInManager<ConsumerUser> signInManager, IMapper mapper,
            IUserClaimsPrincipalFactory<ConsumerUser> userClaimsPrincipalFactory, IAuthorizationService authorizationService, ILookUpSpecification lookUpSpecification) : base(lookUpSpecification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }
        public async Task<IdentityResponseDto> InsertUserAsync(ConsumerUserDto consumerUserDto)
        {
            return await ExecuteWithHandling(async () =>
            {
                ConsumerUser user = _mapper.Map<ConsumerUser>(consumerUserDto);
                IdentityResult identityResult = await _userManager.CreateAsync(user, consumerUserDto.Password);
                if (identityResult.Succeeded)
                    return IdentityResponseDto.Success(_mapper.Map<ConsumerUserDto>(user));
                else
                    return IdentityResponseDto.Failed(identityResult.Errors.ToArray());
            });
        }

        public async Task<IdentityResult> AddToRoleAsync(ConsumerUserDto consumerUserDto, RolesEnum rolesEnum)
        {
            ConsumerUser consumerUser = await FindByIdAsync(consumerUserDto.Id.ToString());
            return await _userManager.AddToRoleAsync(consumerUser, rolesEnum.ToString());
        }
        public async Task<SignInResult> CheckPasswordSignInAsync(ConsumerUserDto consumerUserDto, string password)
        {
            ConsumerUser consumerUser = await FindByIdAsync(consumerUserDto.Id.ToString());
            if (consumerUser == null)
                return SignInResult.Failed;

            return await _signInManager.CheckPasswordSignInAsync(consumerUser, password, lockoutOnFailure: false);
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<ConsumerUserDto>? GetUserByMobile(string mobile)
        {
            ConsumerUser consumerUser = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(mobile));
            return consumerUser != null ? _mapper.Map<ConsumerUserDto>(consumerUser) : null;
        }
        public async Task<ConsumerUserDto>? GetUserById(int id)
        {
            ConsumerUser consumerUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return consumerUser != null ? _mapper.Map<ConsumerUserDto>(consumerUser) : null;
        }
        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await FindByIdAsync(userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }
        /// <summary>
        /// Checks if a user meets a specific authorization policy against the specified resource.
        /// </summary>
        /// <param name="service">The <see cref="IAuthorizationService"/> providing authorization.</param>
        /// <param name="user">The user to evaluate the policy against.</param>
        /// <param name="policyName">The name of the policy to evaluate.</param>
        /// <returns>
        /// A flag indicating whether policy evaluation has succeeded or failed.
        /// This value is <c>true</c> when the user fulfills the policy, otherwise <c>false</c>.
        /// </returns>
        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await FindByIdAsync(userId);
            if (user == null)
                return false;

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);
            return result.Succeeded;
        }

        public Task<bool> IsUserExistAsync(string mobile, string? email)
        {
            return _userManager.Users.AnyAsync(x => x.PhoneNumber.Equals(mobile) || x.Email.Equals(email));
        }

        private async Task<ConsumerUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

    }
}
