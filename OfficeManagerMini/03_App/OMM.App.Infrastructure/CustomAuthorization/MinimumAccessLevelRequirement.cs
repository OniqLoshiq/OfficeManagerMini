using Microsoft.AspNetCore.Authorization;

namespace OMM.App.Infrastructure.CustomAuthorization
{
    public class MinimumAccessLevelRequirement : IAuthorizationRequirement
    {
        public int MinimumAccessLevel { get; private set; }

        public MinimumAccessLevelRequirement(int minimumAccessLevel)
        {
            MinimumAccessLevel = minimumAccessLevel;
        }
    }
}
