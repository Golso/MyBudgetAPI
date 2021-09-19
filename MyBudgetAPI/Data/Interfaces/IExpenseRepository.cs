using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
