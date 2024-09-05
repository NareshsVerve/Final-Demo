using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        //Login user
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {

                var token = await _authService.Login(loginDto);

                return Ok(token);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,"An error occured when authenticating the user.");
                return BadRequest();
            }

        }

        // user Registration
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO UserDto)
        {
            try
            {

                await _authService.Register(UserDto);

                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when registering the user.");
                return BadRequest();
            }

        }
        [HttpPost("Logout")]
        // user Registration
        public async Task<IActionResult> Logout(Guid id)
        {
            try
            {
                await _authService.Logout(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when logging out the user.");
                return BadRequest();
            }

        }
    }
}
