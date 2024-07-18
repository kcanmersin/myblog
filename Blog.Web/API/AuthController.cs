using Microsoft.AspNetCore.Mvc;
using Blog.Entity.DTOs.Users;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Blog.Entity.Entities;

namespace Blog.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;  // Assuming additional user operations might be required

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Consider returning a token or a success message based on your security implementation
                    return Ok("Login successful.");
                }
            }

            return Unauthorized("Invalid login attempt.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully.");
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return Unauthorized("Access denied. You do not have permission to access this resource.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAddDto userAddDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = userAddDto.Email, Email = userAddDto.Email };
            var result = await _userManager.CreateAsync(user, userAddDto.Password);
            if (result.Succeeded)
            {
                // Optionally, sign the user in after successful registration
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("User registered successfully.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
