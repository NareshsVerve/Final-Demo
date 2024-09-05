using Microsoft.AspNetCore.Http.HttpResults;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
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
        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
           var user = await _userRepository.GetUserByIdAsync(id);
            var userDto = UserDTO.Mapping(user);
            return userDto;
        }
        public async Task<UserDTO> GetUserByEmail(string email)
        {
           var user = await _userRepository.GetUserByEmail(email);
            var userDto = UserDTO.Mapping(user);
            return userDto;
        }
        public async Task<UserDTO> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            var userDto = UserDTO.Mapping(user);
            return userDto;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = UserDTO.Mapping(users);
            return userDtos;
        }

        public async Task AddUserAsync(UserDTO userDto)
        {
            var user = UserDTO.Mapping(userDto);
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(UserDTO userDto)
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
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
