using Microsoft.AspNetCore.Authorization;
using OMM.App.Common;
using System;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.CustomAuthorization
{
    internal class MinimumAccessLevelPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(Constants.POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(policyName.Substring(Constants.POLICY_PREFIX.Length), out var accessLevel))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new MinimumAccessLevelRequirement(accessLevel));
                return Task.FromResult(policy.Build());
            }

            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
    }
}
