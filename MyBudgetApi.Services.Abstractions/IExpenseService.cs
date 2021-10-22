using Microsoft.AspNetCore.JsonPatch;
using MyBudgetApi.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseReadDto>> GetAllExpensesAsync();
        Task<ExpenseReadDto> GetExpenseByIdAsync(int id);
        Task<int> CreateExpenseAsync(ExpenseCreateDto expense);
        Task DeleteExpenseAsync(int id);
        Task UpdateExpenseAsync(int id, ExpenseUpdateDto expenseUpdateDto);
        Task PartialUpdateExpenseAsync(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument);
    }
}
