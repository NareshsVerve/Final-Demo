using Microsoft.AspNetCore.Identity;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> GetUserEmailAsync(string Email);
        Task<bool> EmailExist(string email);
        Task<bool> UsernameExist(string username);
        Task<IdentityResult> ResetPasswordAsync(string email, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task AddUserAsync(UserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task DeleteUserAsync(Guid id);
    }
}
