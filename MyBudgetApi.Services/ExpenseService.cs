using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Exceptions;
using MyBudgetApi.Data.Models;
using MyBudgetApi.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Services
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

        public async Task<int> CreateExpenseAsync(ExpenseCreateDto dto)
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

            return await _repository.CreateExpenseAsync(expense);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _repository.GetExpenseByIdAsync(id);

            if (expense is null)
            {
                throw new NotFoundException("Expense not found.");
            }

            if (expense.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Expense of other user.");
            }

            await _repository.DeleteExpenseAsync(expense);
        }

        public async Task<PagedList<ExpenseReadDto>> GetAllExpensesAsync(ExpenseParameters expenseParameters)
        {
            var userId = _userContextService.GetUserId;
            var expenses = await _repository.GetAllExpensesAsync(userId, expenseParameters);
            var expenseList = expenses.Items;
            var dtoList = _mapper.Map<List<ExpenseReadDto>>(expenseList);
            var result = new PagedList<ExpenseReadDto>(dtoList, expenses.TotalCount, expenses.CurrentPage, expenses.PageSize);

            return result;
        }

        public async Task<ExpenseReadDto> GetExpenseByIdAsync(int id)
        {
            var expense = await _repository.GetExpenseByIdAsync(id);

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

        public async Task PartialUpdateExpenseAsync(int id, JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                throw new BadRequestException("patchDocument object is null.");
            }

            var expenseModelFromRepo = await _repository.GetExpenseByIdAsync(id);

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

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateExpenseAsync(int id, ExpenseUpdateDto expenseUpdateDto)
        {
            if (expenseUpdateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }

            var expense = await _repository.GetExpenseByIdAsync(id);

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

            await _repository.SaveChangesAsync();
        }
    }
}
