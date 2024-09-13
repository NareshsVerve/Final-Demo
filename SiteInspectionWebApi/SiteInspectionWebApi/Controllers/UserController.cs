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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Get All Users
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get User By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting user with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchUser([FromQuery] string email = null, [FromQuery] string username = null)
        {
            try
            {
                bool user = false;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    user = await _userService.EmailExist(email);
                }
                else if (!string.IsNullOrWhiteSpace(username))
                {
                    user = await _userService.UsernameExist(username);
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user search.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Create User
        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _userService.EmailExist(userDto.Email))
                {
                    return BadRequest("Email already exists.");
                }
                if (await _userService.UsernameExist(userDto.Username))
                {
                    return BadRequest("Username already exists.");
                }
                await _userService.AddUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPost("Change-Password/{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id, string currentPassword, string newPassword)
        {
            try
            {
                var result = await _userService.ChangePasswordAsync(id, currentPassword, newPassword);
                if (!result.Succeeded)
                {
                    return BadRequest();
                }
                return Ok("Password changes Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating user's password with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    
        
        // Update User
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != userDto.Id)
                {
                    return BadRequest("UserId Mismatch.");
                }
                await _userService.UpdateUserAsync(userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating user with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Delete User
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting user with ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
