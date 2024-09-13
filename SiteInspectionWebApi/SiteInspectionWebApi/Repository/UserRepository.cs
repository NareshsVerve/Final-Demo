using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Data;
using SiteInspectionWebApi.Interface;
using SiteInspectionWebApi.Models.Database_Models;
using SiteInspectionWebApi.Models.DTO;
using SiteInspectionWebApi.Models.Enums;

namespace SiteInspectionWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail && u.IsEmailVerified == true);
        }

        public async Task<User> GetUserEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> EmailExist(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        public async Task<bool> UsernameExist(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            if (user.Role == (int)Role.Inspector)
            {
                user.IsEmailVerified = false;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
          
               _context.Users.Update(user);
               await _context.SaveChangesAsync();
          
        }

        /* public async Task DeleteUserAsync(Guid id)
         {

             var user = await _context.Users.FindAsync(id);
             if (user != null)
             {
                 _context.Users.Remove(user);
                 await _context.SaveChangesAsync();
             }
         }*/

    }
}