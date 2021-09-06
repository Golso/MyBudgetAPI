using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public interface IBudgetRepo
    {
        IEnumerable<Expense> GetAllExpenses();
    }
}
