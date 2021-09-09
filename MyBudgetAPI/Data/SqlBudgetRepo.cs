using AutoMapper;
using MyBudgetAPI.Dtos;
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
        private readonly IMapper _mapper;

        public SqlBudgetRepo(BudgetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public IEnumerable<ExpenseReadDto> GetAllExpenses()
        {
            var expenses = _context.Expenses.ToList();
            
            return _mapper.Map<List<ExpenseReadDto>>(expenses);
        }

        public ExpenseReadDto GetExpenseById(int id)
        {
            var expense = _context.Expenses.Find(id);
            
            return _mapper.Map<ExpenseReadDto>(expense);
        }

        public int CreateExpense(ExpenseCreateDto dto)
        {
            var expense = _mapper.Map<Expense>(dto);
            _context.Expenses.Add(expense);
            _context.SaveChanges();

            return expense.Id;
        }

        public Expense DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);

            if (expense is not null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }

            return expense;
        }
    }
}
