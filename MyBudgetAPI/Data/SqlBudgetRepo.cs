using AutoMapper;
using Microsoft.Extensions.Logging;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
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
        private readonly ILogger<SqlBudgetRepo> _logger;

        public SqlBudgetRepo(BudgetDbContext context, IMapper mapper, ILogger<SqlBudgetRepo> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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

        public void DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }

        public void UpdateExpense(int id, ExpenseUpdateDto expenseUpdateDto)
        {
            var expense = _context.Expenses.Find(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            expense.Amount = expenseUpdateDto.Amount;
            expense.Category = expenseUpdateDto.Category;
            expense.Description = expenseUpdateDto.Description;

            _context.SaveChanges();
        }
    }
}
