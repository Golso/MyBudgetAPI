using Microsoft.EntityFrameworkCore;
using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Abstractions;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Repositories
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

        public async Task<PagedList<User>> GetAllUsersAsync(AccountParameters accountParameters)
        {
            var accounts = await PagedList<User>
                .ToPagedList(_context.Users.OrderBy(e => e.Id),
                accountParameters.PageNumber,
                accountParameters.PageSize);

            return accounts;
        }

        public async Task RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
