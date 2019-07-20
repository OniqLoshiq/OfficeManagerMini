using Microsoft.AspNetCore.Authorization;
using OMM.App.Infrastructure.Common;
using System;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.CustomAuthorization
{
    public class MinimumAccessLevelPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(InfrastructureConstants.POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(policyName.Substring(InfrastructureConstants.POLICY_PREFIX.Length), out var accessLevel))
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
