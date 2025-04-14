using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.DTOs;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _userService.GetUserProfileAsync(GetUserId());

            return profile == null ? NotFound("User not found.") : Ok(profile);
        }

        [HttpPost("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDTO dto)
        {
            var profileUpdated = await _userService.UpdateUserProfileAsync(GetUserId(), dto);
            return profileUpdated ? Ok("User updated.") : NotFound("Profile not found.");
        }
    }
}
