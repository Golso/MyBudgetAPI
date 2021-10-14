using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;

namespace MyBudgetAPI.Data
{
    public interface IExpenseRepository
    {
        IEnumerable<ExpenseReadDto> GetAllExpenses();
        ExpenseReadDto GetExpenseById(int id);
        int CreateExpense(ExpenseCreateDto expense);
        void DeleteExpense(int id);
        void UpdateExpense(int id, ExpenseUpdateDto expenseUpdateDto);
        void PartialUpdateExpense(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument);
    }
}
