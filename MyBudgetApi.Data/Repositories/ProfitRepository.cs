using Microsoft.EntityFrameworkCore;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Repositories
{
    public class ProfitRepository : IProfitRepository
    {
        private readonly BudgetDbContext _context;

        public ProfitRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProfitAsync(Profit profit)
        {
            await _context.Profits.AddAsync(profit);
            await _context.SaveChangesAsync();

            return profit.Id;
        }

        public async Task DeleteProfitAsync(Profit profit)
        {
            _context.Profits.Remove(profit);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profit>> GetAllProfitsAsync(int userId)
        {
            var profits = await _context.Profits.Where(p => p.UserId == userId).ToListAsync();

            return profits;
        }

        public async Task<Profit> GetProfitByIdAsync(int id)
        {
            var profit = await _context.Profits.FindAsync(id);

            return profit;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
