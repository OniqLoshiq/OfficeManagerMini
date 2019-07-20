using Microsoft.AspNetCore.Authorization;
using OMM.App.Infrastructure.Common;

namespace OMM.App.Infrastructure.CustomAuthorization
{
    public class MinimumAccessLevelAttribute : AuthorizeAttribute
    {
        public MinimumAccessLevelAttribute(int accessLevel) => AccessLevel = accessLevel;

        public int AccessLevel
        {
            get
            {
                if (int.TryParse(Policy.Substring(InfrastructureConstants.POLICY_PREFIX.Length), out var accessLevel))
                {
                    return accessLevel;
                }
                return default;
            }
            set
            {
                Policy = $"{InfrastructureConstants.POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}
