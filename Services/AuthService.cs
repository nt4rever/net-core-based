using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Entities;
using net_core_based.Helpers.Messages;
using net_core_based.Models;

namespace net_core_based.Services
{
    public interface IAuthService
    {
        public Task<ActionResult<AuthenticationResponse>> Login(UserLogin model);
        public Task<IActionResult> Register(UserRegistration model);
        public Task<IActionResult> GetUserRoles(string username);
        public Task<IActionResult> CreateRole(string roleName);
        public Task<IActionResult> AddUserToRole(string username, string roleName);
        public Task<IActionResult> RemoveUserFromRole(string username, string roleName);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, JwtService jwtService, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtService = jwtService;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task<ActionResult<AuthenticationResponse>> Login(UserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                _logger.LogInformation(1, "Generate new token");
                return await _jwtService.CreateToken(user);
            }
            return new UnauthorizedResult();
        }

        public async Task<IActionResult> Register(UserRegistration model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new ObjectResult(new ErrorResponse() { Message = AuthMessages.USER_EXSITS, StatusCode = StatusCodes.Status403Forbidden })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new ObjectResult(new ErrorResponse() { Message = AuthMessages.CREATE_USER_FAILED, StatusCode = StatusCodes.Status403Forbidden })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

            return new ObjectResult(new
            {
                Message = AuthMessages.CREATE_USER_SUCCESS,
                StatusCode = StatusCodes.Status201Created
            })
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Roles Added");
                    return new OkObjectResult(new { result = $"Role {roleName} added successfully" });
                }
                else
                {
                    _logger.LogInformation(2, "Error");
                    return new BadRequestObjectResult(new { error = $"Issue adding the new {roleName} role" });
                }
            }
            return new BadRequestObjectResult(new { error = "Role already exist" });
        }

        public async Task<IActionResult> GetUserRoles(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new NotFoundResult();
            var roles = await _userManager.GetRolesAsync(user);
            return new OkObjectResult(roles);
        }

        public async Task<IActionResult> AddUserToRole(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.UserName} added to the {roleName} role");
                    return new OkObjectResult(new { result = $"User {user.UserName} added to the {roleName} role" });
                }
                else
                {
                    _logger.LogInformation(1, $"Error: Unable to add user {user.UserName} to the {roleName} role");
                    return new BadRequestObjectResult(new { error = $"Error: Unable to add user {user.UserName} to the {roleName} role" });
                }
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> RemoveUserFromRole(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} removed from the {roleName} role");
                    return new OkObjectResult(new { result = $"User {user.Email} removed from the {roleName} role" });
                }
                else
                {
                    _logger.LogInformation(1, $"Error: Unable to removed user {user.Email} from the {roleName} role");
                    return new BadRequestObjectResult(new { error = $"Error: Unable to removed user {user.Email} from the {roleName} role" });
                }
            }
            return new NotFoundResult();
        }
    }
}
