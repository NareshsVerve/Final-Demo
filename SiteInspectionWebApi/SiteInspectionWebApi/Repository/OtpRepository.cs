using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Data;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Repository
{
    public class OtpRepository:IOtpRepository
    {
        private readonly ApplicationDbContext _context; 
        public OtpRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Otp> GetOtpByUserIdAsync(Guid userId)
        {
            return await _context.Otps.FirstOrDefaultAsync(otp => otp.UserId == userId);
        }
        public async Task CreateOtpAsync(Otp otp)
        {
             await _context.AddAsync(otp);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOtpAsync(Otp otp)
        {
             _context.Remove(otp);
            await _context.SaveChangesAsync();
        }
    }
}
