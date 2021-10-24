using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Models;
using System.Threading.Tasks;

namespace MyBudgetApi.Data
{
    public interface IExpenseRepository
    {
        Task<PagedList<Expense>> GetAllExpensesAsync(int userId, ExpenseParameters expenseParameters);
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<int> CreateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(Expense expense);
        Task SaveChangesAsync();
    }
}
