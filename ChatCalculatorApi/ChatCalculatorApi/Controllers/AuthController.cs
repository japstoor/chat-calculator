using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            var user = _authService.Login(request.Username, request.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                userId = user.UserId,
                username = user.Username,
                token = token
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthRequest request)
        {
            var success = _authService.Register(request.Username, request.Password);
            if (!success) return BadRequest(new { message = "User already exists" });

            return Ok(new { message = "Registered successfully" });
        }

    }
}
