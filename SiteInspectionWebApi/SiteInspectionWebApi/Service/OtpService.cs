using Microsoft.AspNetCore.Identity;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Service
{
    public class OtpService:IOtpService
    {
        private readonly IOtpRepository _otpRepository;
        private readonly TimeSpan _otpValidityDuration = TimeSpan.FromMinutes(5);
        public OtpService(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }
        public async Task<int> GenerateOtpAsync(Guid userId)
        {
            var random = new Random();
            int otpCode = random.Next(100000, 999999);

            var otp = new Otp
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Code = otpCode,
                ExpirationTime = DateTime.Now.Add(_otpValidityDuration)
            };

            // Save the OTP to the database
            await _otpRepository.CreateOtpAsync(otp);
            return otpCode;
        }

        public async Task<IdentityResult> ValidateOtpAsync(Guid userId, int otpCode)
        {
            var otp = await _otpRepository.GetOtpByUserIdAsync(userId);
            if (otp == null || otp.Code != otpCode)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Otp not matched." }); 
            }

            if (otp.ExpirationTime < DateTime.UtcNow)
            {
                
                await _otpRepository.DeleteOtpAsync(otp);
                return IdentityResult.Failed(new IdentityError { Description = "Otp is expired.." });
            }
            await _otpRepository.DeleteOtpAsync(otp);
            return IdentityResult.Success;
        }
    }
}
