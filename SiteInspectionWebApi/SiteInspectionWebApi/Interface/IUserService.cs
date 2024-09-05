using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<UserDTO> GetUserByEmail(string email);
        Task<UserDTO> GetUserByUsername(string username);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserDTO user);
        Task UpdateUserAsync(UserDTO user);
        Task DeleteUserAsync(Guid id);
    }
}
