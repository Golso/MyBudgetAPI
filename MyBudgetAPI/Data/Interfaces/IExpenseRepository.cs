using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<ExpenseReadDto>> GetAllExpenses();
        Task<ExpenseReadDto> GetExpenseById(int id);
        Task<int> CreateExpense(ExpenseCreateDto expense);
        Task DeleteExpense(int id);
        Task UpdateExpense(int id, ExpenseUpdateDto expenseUpdateDto);
        Task PartialUpdateExpense(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument);
    }
}
