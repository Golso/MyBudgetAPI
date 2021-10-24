using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly BudgetDbContext _context;

        public ExpenseRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Expense>> GetAllExpensesAsync(int userId, ExpenseParameters expenseParameters)
        {
            var expense = await PagedList<Expense>
                .ToPagedList(_context.Expenses.Where(e => e.UserId == userId).OrderBy(e => e.Id),
                expenseParameters.PageNumber, expenseParameters.PageSize);

            return expense;
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            return expense;
        }

        public async Task<int> CreateExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            return expense.Id;
        }

        public async Task DeleteExpenseAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
