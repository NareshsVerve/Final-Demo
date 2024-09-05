using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<bool> EmailExist(string email);
        Task<bool> UsernameExist(string username);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task DeleteUserAsync(Guid id);
    }
}
