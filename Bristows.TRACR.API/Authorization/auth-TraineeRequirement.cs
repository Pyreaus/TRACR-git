using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bristows.TRACR.API.Authorization
{
    public class TraineeRequirement : IAuthorizationRequirement { }
    public class TraineeRequirementHandler : AuthorizationHandler<TraineeRequirement>
    {
        private readonly IUserService _userService;
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly ILogger<TraineeRequirementHandler> _logger;
        public TraineeRequirementHandler(ILogger<TraineeRequirementHandler> logger, ClaimsPrincipal claimsPrincipal, IUserService userService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _claimsPrincipal = claimsPrincipal ?? throw new ArgumentNullException(nameof(claimsPrincipal));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger.LogInformation("----->{CP}",_claimsPrincipal.FindFirst("DomainUsername"));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TraineeRequirement requirement)
        {
            if (_claimsPrincipal.Identity?.IsAuthenticated == true)
            {
                Claim? usernameClaim = _claimsPrincipal.FindFirst("DomainUsername");
                if (usernameClaim?.Value != null)
                {
                    PeopleFinderUser? user = await _userService.GetByDomainAsync(usernameClaim.Value);
                    if (user != null && await _userService.IsTraineePfidAsync((int)user!.OtherPfid))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    else
                    {
                        _logger.LogWarning("User is not reviewerer");
                        context.Fail();
                    }
                }
            }
            else { context.Fail(); }
        }
    }
}
