using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Services;

namespace net_core_based.Controllers
{
    [Route("api/claim")]
    [ApiController]
    public class ClaimSetupController : ControllerBase
    {
        private readonly IClaimService _claimService;
        public ClaimSetupController(IClaimService claimService) { _claimService = claimService; }

        [HttpPost("add-claim-to-user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AddClaimToUser(string username, string claimName, string value)
        {
            return await _claimService.AddClaimToUser(username, claimName, value);
        }

        [HttpGet]
        [Route("get-all-claims")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAllUserClaims(string username)
        {
            return await _claimService.GetAllClaims(username);
        }

        [HttpDelete]
        [Route("remove-user-claim")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoveUsersClaim(string username, string claimName, string value)
        {
            return await _claimService.RemoveClaim(username, claimName, value);
        }
    }
}
