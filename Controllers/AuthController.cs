using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly DatabaseService _databaseService;

        public AuthController(AuthService authService, DatabaseService databaseService)
        {
            _authService = authService;
            _databaseService = databaseService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            // In a real app, validate against database
            // This is simplified for example
            if (userLogin.Username == "admin" && userLogin.Password == "admin123")
            {
                var user = new User { Username = userLogin.Username, Role = "Admin" };
                var token = _authService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            else if (userLogin.Username == "user" && userLogin.Password == "user123")
            {
                var user = new User { Username = userLogin.Username, Role = "User" };
                var token = _authService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}