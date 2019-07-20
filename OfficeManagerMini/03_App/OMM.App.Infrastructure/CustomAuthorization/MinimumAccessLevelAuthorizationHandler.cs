using Microsoft.AspNetCore.Authorization;
using OMM.App.Infrastructure.Common;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.CustomAuthorization
{
    public class MinimumAccessLevelAuthorizationHandler : AuthorizationHandler<MinimumAccessLevelRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAccessLevelRequirement requirement)
        {
            var accessLevelClaim = context.User.FindFirst(c =>
                c.Type == InfrastructureConstants.ACCESS_LEVEL_CLAIM);

            if (accessLevelClaim != null)
            {
                var accessLevel = int.Parse(accessLevelClaim.Value);

                if (accessLevel >= requirement.MinimumAccessLevel)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
