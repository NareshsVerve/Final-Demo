using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface IOtpRepository
    {
        Task<Otp> GetOtpByUserIdAsync(Guid userId);
        Task CreateOtpAsync(Otp otp);
        Task DeleteOtpAsync(Otp otp);
    }
}
