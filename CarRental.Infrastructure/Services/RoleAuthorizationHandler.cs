using CarRental.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Services;
public class RoleAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
{
    private readonly UserManager<ApplicationUser> userManager;

    public RoleAuthorizationHandler(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var user = await userManager.FindByNameAsync(context.User.Identity.Name);
            var roles = await userManager.GetRolesAsync(user);

            if (requirement.AllowedRoles.Intersect(roles).Any())
            {
                context.Succeed(requirement);
                return;
            }
        }

        context.Fail();
    }
}
