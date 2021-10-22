using Microsoft.EntityFrameworkCore;
using MyBudgetAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly BudgetDbContext _context;

        public ExpenseRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync(int userId)
        {
            var expenses = await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();

            return expenses;
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
