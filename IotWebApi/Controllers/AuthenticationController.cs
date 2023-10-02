using IotWebApi.Dto;
using IotWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IotWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequestDto model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return Unauthorized(new { message = "Email or password is incorrect" });
            // return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        [HttpPost("register")]
        public IActionResult Register(UserDto model, string password)
        {
            var response = _userService.Create(model, password);
            if (!string.IsNullOrEmpty(response)) return Ok(new { message = "User registered successfully", state = 1 });
            return BadRequest(new { message = "The email is in use!", state = 0 });
        }
    }
}
