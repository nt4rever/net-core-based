using Microsoft.AspNetCore.Mvc;
using net_core_based.Models;
using net_core_based.Services;

namespace net_core_based.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            return await _authService.Login(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistration model)
        {
            return await _authService.Register(model);
        }
    }
}
