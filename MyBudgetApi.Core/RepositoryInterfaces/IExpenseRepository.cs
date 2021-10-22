using MyBudgetApi.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync(int userId);
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<int> CreateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(Expense expense);
        Task SaveChangesAsync();
    }
}
