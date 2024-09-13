using Microsoft.AspNetCore.Identity;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Interface
{
    public interface IOtpService
    {
        Task<int> GenerateOtpAsync(Guid userId);
        Task<IdentityResult> ValidateOtpAsync(Guid userId, int otpCode);
    }
}
