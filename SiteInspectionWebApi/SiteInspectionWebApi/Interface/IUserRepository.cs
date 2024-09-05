using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task<bool>EmailExist(string email);
        Task<bool> UsernameExist(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
