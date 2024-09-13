using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using SiteInspectionWebApi.Helper;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Models.Enums;
using SiteInspectionWebApi.Repository;

namespace SiteInspectionWebApi.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            if (users == null)
            {
                throw new Exception("User not found");
            }
            var userDtos = UserDTO.Mapping(users);
            return userDtos;
        }
        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
           var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var userDto = UserDTO.Mapping(user);
            return userDto;
        }
        public async Task<UserDTO> GetUserEmailAsync(string Email)
         {
           var user = await _userRepository.GetUserEmailAsync(Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
             var userDto = UserDTO.Mapping(user);
            return userDto;
        }

        public async Task<bool> EmailExist(string email)
        { 
            return await _userRepository.EmailExist(email);
        }
        public async Task<bool> UsernameExist(string username)
        {
            return await _userRepository.UsernameExist(username);
        }
        public async Task<IdentityResult> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userRepository.GetUserEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            user.PasswordHash = PasswordHasher.HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(user);
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            if (user.PasswordHash != PasswordHasher.HashPassword (currentPassword))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Old Password is not Matched." });
            }

            user.PasswordHash = PasswordHasher.HashPassword(newPassword);
            await _userRepository.UpdateUserAsync(user);
            return IdentityResult.Success;
        }
        public async Task AddUserAsync(UserDTO userDto)
        {
            userDto.Id = Guid.NewGuid();
            userDto.CreatedDate = DateTime.Now;
            userDto.UpdatedDate = userDto.CreatedDate;
            userDto.UpdatedBy = userDto.CreatedBy;
            userDto.Password = PasswordHasher.HashPassword(userDto.Password);
            userDto.ProfileImage = string.IsNullOrWhiteSpace(userDto.ProfileImage) ? null : userDto.ProfileImage;
            
            var user = UserDTO.Mapping(userDto);
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Username = userDto.Username;
            existingUser.Email = userDto.Email; 
            existingUser.IsEmailVerified = userDto.IsEmailVerified;
            existingUser.ProfileImage = userDto.ProfileImage;
            existingUser.UpdatedBy = userDto.UpdatedBy;
            existingUser.UpdatedDate = DateTime.Now; 
            existingUser.IsActive = userDto.IsActive;
            existingUser.Role = (int)userDto.Role;
            
            await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task DeleteUserAsync(Guid id)
        {

            var existingUser = await _userRepository.GetUserByIdAsync(id);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            existingUser.IsActive = false;
            await _userRepository.UpdateUserAsync(existingUser);
        }
    }
}
