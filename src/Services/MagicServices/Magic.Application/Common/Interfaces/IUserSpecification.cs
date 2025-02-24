using Magic.Application.Common.Enums;

namespace Magic.Application.Common.Interfaces;
public interface IUserSpecification
{
    Task<IdentityResponseDto> InsertUserAsync(ConsumerUserDto consumerUserDto);
    Task<IdentityResult> AddToRoleAsync(ConsumerUserDto consumerUserDto, RolesEnum rolesEnum);
    Task<SignInResult> CheckPasswordSignInAsync(ConsumerUserDto consumerUserDto, string password);
    Task<ConsumerUserDto>? GetUserByMobile(string mobile);
    Task<ConsumerUserDto>? GetUserById(int id);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<bool> IsUserExistAsync(string mobile, string? email);
    Task Logout();
}
