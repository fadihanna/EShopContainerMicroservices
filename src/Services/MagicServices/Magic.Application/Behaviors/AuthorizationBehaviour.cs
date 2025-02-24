using Magic.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Magic.Application.Behaviors;

public class AuthorizationBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IUser _user;
    private readonly IUserSpecification _userSpecification;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthorizationBehaviour(
        IUser user,
        IUserSpecification userSpecification, IHttpContextAccessor httpContextAccessor)
    {
        _user = user;
        _userSpecification = userSpecification;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_user.Id == null)
                throw new ForbiddenAccessException(InternalErrorCode.Status401Unauthorized);
            
            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = await _userSpecification.IsInRoleAsync(_user.Id, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                // Must be a member of at least one role in roles
                if (!authorized)
                    throw new ForbiddenAccessException(InternalErrorCode.Status403Forbidden);
            }

            // Policy-based authorization
            //ASP.NET Core Identity does not store policies directly in the Identity database tables.
            //Policies are usually defined in the application code and are stored in memory at runtime,
            //but are defined and configured in the Startup.cs or Program.cs file of the application, as shown in the examples above.
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _userSpecification.AuthorizeAsync(_user.Id, policy);

                    if (!authorized)
                        throw new ForbiddenAccessException(InternalErrorCode.Status403Forbidden);
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
