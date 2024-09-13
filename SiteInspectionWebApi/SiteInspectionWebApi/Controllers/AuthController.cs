using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Service;

namespace SiteInspectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IOtpService _otpService;
        private readonly EmailServices _emailService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger,
            IUserService userService, IOtpService otpService, EmailServices emailService
            )
        {
            _authService = authService;
            _logger = logger;
            _userService = userService;
            _otpService = otpService;
            _emailService = emailService;
        }

        // user Registration
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            try
            {
                await _authService.Register(userDto);

                return CreatedAtAction(actionName: nameof(UserController.GetUserById),controllerName: "User",                          
            routeValues: new { id = userDto.Id },        
            value: userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when registering the user.");
                return BadRequest();
            }

        }
        //Login user
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {

                var token = await _authService.Login(loginDto);
                if(token == null)
                {
                    return Unauthorized("Invalid Credential or Your mail is not verified.");
                }
                return Ok(token);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,"An error occured when authenticating the user.");
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

                return Ok("Logout Successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when logging out the user.");
                return BadRequest();
            }

        }

        [HttpGet("Send-otp/{email}")]
        public async Task<IActionResult> SendOtp(string email)
       {
            try
            {
                var user =await _userService.GetUserEmailAsync(email);
                var otp = await _otpService.GenerateOtpAsync(user.Id);
                await _emailService.SendOtpEmailAsync(email,otp.ToString());
                return Ok("OTP has been sent to your email");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when sending otp for the user.");
                return BadRequest();
            }

        }
        [HttpPost("Verify-email")]
        public async Task<IActionResult> Verify(Guid id, int otp)
        {
            try
            {
                var isValid = await _otpService.ValidateOtpAsync(id, otp);
                if (isValid.Succeeded)
                {
                    var user = await _userService.GetUserByIdAsync(id);
                    if(user.IsEmailVerified == false)
                    {
                        user.IsEmailVerified = true;
                        var updateUser = UpdateUserDTO.Mapping(user);
                        await _userService.UpdateUserAsync(updateUser);
                    }
                    return Ok("Otp Verify Sucessful.");
                }
                return BadRequest("Invalid OTP or OTP expired.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when logging out the user.");
                return BadRequest();
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
        {
            var user = await _userService.GetUserEmailAsync(request.Email);
            var isValid = await _otpService.ValidateOtpAsync(user.Id, request.OtpCode);
            if (!isValid.Succeeded)
            {
                return BadRequest("Invalid or expired OTP.");
            }

            var result = await _userService.ResetPasswordAsync(request.Email, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to reset password.");
            }

            return Ok("Password has been successfully reset.");
        }
    }
}
