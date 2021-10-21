using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Data;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ExpenseService(IExpenseRepository repository, IMapper mapper, IUserContextService userContextService)
        {
            _repository = repository;
            _mapper = mapper;
            _userContextService = userContextService;
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

            return await _repository.CreateExpense(expense);
        }

        public async Task DeleteExpense(int id)
        {
            var expense = await _repository.GetExpenseById(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            if (expense.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Expense of other user.");
            }

            await _repository.DeleteExpense(expense);
        }

        public async Task<IEnumerable<ExpenseReadDto>> GetAllExpenses()
        {
            var userId = _userContextService.GetUserId;
            var expenses = await _repository.GetAllExpenses(userId);

            return _mapper.Map<List<ExpenseReadDto>>(expenses);
        }

        public async Task<ExpenseReadDto> GetExpenseById(int id)
        {
            var expense = await _repository.GetExpenseById(id);

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

        public async Task PartialUpdateExpense(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                throw new BadRequestException("patchDocument object is null.");
            }

            var expenseModelFromRepo = await _repository.GetExpenseById(id);

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

            await _repository.UpdateExpense();
        }

        public async Task UpdateExpense(int id, ExpenseUpdateDto expenseUpdateDto)
        {
            if (expenseUpdateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }

            var expense = await _repository.GetExpenseById(id);

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

            await _repository.UpdateExpense();
        }
    }
}
