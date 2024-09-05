using SiteInspectionWebApi.Models.DTO;

namespace SiteInspectionWebApi.Interface
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO loginDto);
        Task Logout(Guid id);
        Task Register (UserDTO userDTO);

    }
}
