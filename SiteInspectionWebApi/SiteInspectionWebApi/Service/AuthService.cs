using Microsoft.AspNetCore.Http.HttpResults;
using SiteInspectionWebApi.Helper;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Service
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtToken _tokenService;
        public AuthService(IUserRepository userRepository,
            JwtToken tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string> Login(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(loginDto.UsernameOrEmail);
            var hashPassword = PasswordHasher.HashPassword(loginDto.Password);
            if (user != null && user.PasswordHash == hashPassword)
            { 
                var token = _tokenService.GenerateToken(user);
                user.Token = token;
                await _userRepository.UpdateUserAsync(user);
                return token;
            }
            else return null;
        }
   
        public async Task Register(UserDTO userDto)
        {
            userDto.Id= Guid.NewGuid();
            userDto.CreatedDate = DateTime.Now;
            userDto.UpdatedDate = userDto.CreatedDate;
            userDto.CreatedBy = userDto.Id;
            userDto.UpdatedBy = userDto.CreatedBy;
            userDto.Password = PasswordHasher.HashPassword(userDto.Password);
            userDto.ProfileImage = string.IsNullOrWhiteSpace(userDto.ProfileImage) ? null : userDto.ProfileImage;

            var user = UserDTO.Mapping(userDto);
            await _userRepository.AddUserAsync(user);
        }
        public async Task Logout(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            user.Token = null;
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
