using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class SqlBudgetRepo : IBudgetRepo
    {
        private readonly BudgetDbContext _context;

        public SqlBudgetRepo(BudgetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _context.Expenses.ToList();
        }

        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.Find(id);
        }

        public void CreateExpense(Expense expense)
        {
            if (expense is null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        public void DeleteExpense(Expense expense)
        {
            if (expense is null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
    }
}
