using Microsoft.EntityFrameworkCore;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BudgetDbContext _context;

        public AccountRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var _user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            return _user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
