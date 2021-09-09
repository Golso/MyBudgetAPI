using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public interface IBudgetRepo
    {
        IEnumerable<ExpenseReadDto> GetAllExpenses();
        ExpenseReadDto GetExpenseById(int id);
        int CreateExpense(ExpenseCreateDto expense);
        Expense DeleteExpense(int id);
    }
}
