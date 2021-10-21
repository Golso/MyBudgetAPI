using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenses(int userId);
        Task<Expense> GetExpenseById(int id);
        Task<int> CreateExpense(Expense expense);
        Task DeleteExpense(Expense expense);
        Task UpdateExpense();
    }
}
