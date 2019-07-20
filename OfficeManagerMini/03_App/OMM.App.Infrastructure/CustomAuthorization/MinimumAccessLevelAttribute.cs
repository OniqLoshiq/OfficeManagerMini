using Microsoft.AspNetCore.Authorization;
using OMM.App.Common;

namespace OMM.App.Infrastructure.CustomAuthorization
{
    internal class MinimumAccessLevelAttribute : AuthorizeAttribute
    {
        public MinimumAccessLevelAttribute(int accessLevel) => AccessLevel = accessLevel;

        public int AccessLevel
        {
            get
            {
                if (int.TryParse(Policy.Substring(Constants.POLICY_PREFIX.Length), out var accessLevel))
                {
                    return accessLevel;
                }
                return default;
            }
            set
            {
                Policy = $"{Constants.POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}
