using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Models;
using net_core_based.Services;

namespace net_core_based.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns>A jwt token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/auth/login
        ///     {
        ///        "username": "tan.le",
        ///        "password": "Abcd1234@@"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly jwt token</response>
        /// <response code="400">If validation body error</response>
        /// <response code="401">If the credential not valid</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserLogin model)
        {
            return await _authService.Login(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistration model)
        {
            return await _authService.Register(model);
        }
        
        [HttpPost]
        [Route("create-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            return await _authService.CreateRole(roleName);
        }

        [HttpGet("user-roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserRoles(string username)
        {
            return await _authService.GetUserRoles(username);
        }
        
        [HttpPost("add-user-to-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToRole(string username, string rolename)
        {
            return await _authService.AddUserToRole(username, rolename);
        }

        [HttpPost("remove-user-from-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUserFromRole(string username, string rolename)
        {
            string? currentUser = HttpContext.User?.Identity?.Name;
            if (currentUser == username) return Forbid();
            return await _authService.RemoveUserFromRole(username, rolename);
        }
    }
}
