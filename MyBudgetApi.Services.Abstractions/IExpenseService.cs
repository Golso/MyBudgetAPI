using Microsoft.AspNetCore.JsonPatch;
using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Dtos;
using System.Threading.Tasks;

namespace MyBudgetApi.Services.Abstractions
{
    public interface IExpenseService
    {
        Task<PagedList<ExpenseReadDto>> GetAllExpensesAsync(ExpenseParameters expenseParameters);
        Task<ExpenseReadDto> GetExpenseByIdAsync(int id);
        Task<int> CreateExpenseAsync(ExpenseCreateDto expense);
        Task DeleteExpenseAsync(int id);
        Task UpdateExpenseAsync(int id, ExpenseUpdateDto expenseUpdateDto);
        Task PartialUpdateExpenseAsync(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument);
    }
}
