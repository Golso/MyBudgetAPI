using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Models;
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

        public async Task<PagedList<Profit>> GetAllProfitsAsync(int userId, ProfitParameters profitParameters)
        {
            var profits = await PagedList<Profit>
               .ToPagedList(_context.Profits.Where(e => e.UserId == userId 
               && e.Amount <= profitParameters.MaxAmount && e.Amount >= profitParameters.MinAmount
               && e.Date <= profitParameters.MaxDate && e.Date >= profitParameters.MinDate
               ).OrderBy(e => e.Id),
               profitParameters.PageNumber, profitParameters.PageSize);

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
