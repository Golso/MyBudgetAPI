using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyBudgetAPI.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseRepository(BudgetDbContext context, IMapper mapper)
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

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            return _mapper.Map<ExpenseReadDto>(expense);
        }

        public int CreateExpense(ExpenseCreateDto dto)
        {
            //No idea why required attribute is not working
            if (dto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }
            if (dto.Date == DateTime.MinValue)
            {
                throw new BadRequestException("Date is required.");
            }

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
            if (expenseUpdateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }

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

        public void PartialUpdateExpense(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                throw new BadRequestException("patchDocument object is null.");
            }

            var expenseModelFromRepo = _context.Expenses.Find(id);

            if (expenseModelFromRepo is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            var expenseToPatch = _mapper.Map<ExpenseUpdateDto>(expenseModelFromRepo);
            patchDocument.ApplyTo(expenseToPatch);

            _mapper.Map(expenseToPatch, expenseModelFromRepo);

            _context.SaveChanges();
        }
    }
}
