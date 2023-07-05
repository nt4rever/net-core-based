using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Entities;
using System.Security.Claims;

namespace net_core_based.Services
{
    public interface IClaimService
    {
        public Task<IActionResult> GetAllClaims(string username);
        public Task<IActionResult> AddClaimToUser(string username, string claimName, string value);
        public Task<IActionResult> RemoveClaim(string username, string claimName, string value);
    }

    public class ClaimService : IClaimService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClaimService> _logger;

        public ClaimService(UserManager<ApplicationUser> userManager, ILogger<ClaimService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> AddClaimToUser(string username, string claimName, string value)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new NotFoundResult();
            }
            var result = await _userManager.AddClaimAsync(user, new Claim(claimName, value));
            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"the claim {claimName} add to the  User {user.Email}");
                return new OkObjectResult(new { result = $"the claim {claimName} add to the  User {user.Email}" });
            }
            else
            {
                _logger.LogInformation(1, $"Error: Unable to add the claim {claimName} to the  User {user.Email}");
                return new BadRequestObjectResult(new { error = $"Error: Unable to add the claim {claimName} to the  User {user.Email}" });
            }
        }

        public async Task<IActionResult> GetAllClaims(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null) return new NotFoundResult();
            var claims = await _userManager.GetClaimsAsync(user);
            return new OkObjectResult(claims);  
        }

        public async Task<IActionResult> RemoveClaim(string username, string claimName, string value)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null) return new NotFoundResult();
            await _userManager.RemoveClaimAsync(user, new Claim(claimName, value));
            return new OkResult();
        }
    }
}
