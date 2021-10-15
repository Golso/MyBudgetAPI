using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ExpenseRepository(BudgetDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<ExpenseReadDto>> GetAllExpenses()
        {
            var expenses = await _context.Expenses.Where(e => e.UserId == _userContextService.GetUserId).ToListAsync();

            return _mapper.Map<List<ExpenseReadDto>>(expenses);
        }

        public async Task<ExpenseReadDto> GetExpenseById(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            if (expense.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Expense of other user.");
            }

            return _mapper.Map<ExpenseReadDto>(expense);
        }

        public async Task<int> CreateExpense(ExpenseCreateDto dto)
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
            expense.UserId = _userContextService.GetUserId;
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            return expense.Id;
        }

        public async Task DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            if (expense.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Expense of other user.");
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpense(int id, ExpenseUpdateDto expenseUpdateDto)
        {
            if (expenseUpdateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }

            var expense = await _context.Expenses.FindAsync(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            if (expense.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Expense of other user.");
            }

            expense.Amount = expenseUpdateDto.Amount;
            expense.Category = expenseUpdateDto.Category;
            expense.Description = expenseUpdateDto.Description;

            await _context.SaveChangesAsync();
        }

        public async Task PartialUpdateExpense(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                throw new BadRequestException("patchDocument object is null.");
            }

            var expenseModelFromRepo = await _context.Expenses.FindAsync(id);

            if (expenseModelFromRepo is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            if (expenseModelFromRepo.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Expense of other user.");
            }

            var expenseToPatch = _mapper.Map<ExpenseUpdateDto>(expenseModelFromRepo);
            patchDocument.ApplyTo(expenseToPatch);

            _mapper.Map(expenseToPatch, expenseModelFromRepo);

            await _context.SaveChangesAsync();
        }
    }
}
