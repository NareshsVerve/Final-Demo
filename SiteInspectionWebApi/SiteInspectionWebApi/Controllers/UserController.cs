using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteInspectionWebApi.Helper;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Service;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Get All Users
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Get User By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Get User By Email or Username
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] string email = null, [FromQuery] string username = null)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Email or Username must be provided.");
            }
            UserDTO user = null;
            if (!string.IsNullOrWhiteSpace(email))
            {
                user = await _userService.GetUserByEmail(email);
            }
            else if (!string.IsNullOrWhiteSpace(username))
            {
                user = await _userService.GetUserByUsername(username);
            }
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Create User
        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _userService.GetUserByEmail(userDTo.Email) != null)
            {
                return BadRequest("Email already exists.");
            }
            if (await _userService.GetUserByUsername(userDTo.Username) != null)
            {
                return BadRequest("Username already exists.");
            }

            userDTo.Id = Guid.NewGuid();
            userDTo.CreatedDate = DateTime.Now;
            userDTo.UpdatedDate = userDTo.CreatedDate;
            userDTo.UpdatedBy = userDTo.CreatedBy;
            userDTo.Password = PasswordHasher.HashPassword(userDTo.Password);
            userDTo.ProfileImage = string.IsNullOrWhiteSpace(userDTo.ProfileImage) ? null : userDTo.ProfileImage;

            await _userService.AddUserAsync(userDTo);
            return CreatedAtAction(nameof(GetUserById), new { id = userDTo.Id }, userDTo);
        }

        // Update User
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,[FromBody] UserDTO userDTo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != userDTo.Id)
            {
                return BadRequest("UserId Mismatch.");
            }
            var existingUser = await _userService.GetUserByIdAsync(userDTo.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            if (existingUser.Email != userDTo.Email && await _userService.GetUserByEmail(userDTo.Email) != null)
            {
                return BadRequest("Email already exists.");
            }

            if (existingUser.Username != userDTo.Username && await _userService.GetUserByUsername(userDTo.Username) != null)
            {
                return BadRequest("Username already exists.");
            }
         
            existingUser.UpdatedDate = DateTime.Now;
            await _userService.UpdateUserAsync(existingUser);
            return NoContent();
        }

        // Soft Delete User
        [HttpPatch("soft-delete/{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = false;
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        // Delete User
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
