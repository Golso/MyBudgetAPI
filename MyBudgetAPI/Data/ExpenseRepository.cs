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

        public ExpenseRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses(int userId)
        {
            var expenses = await _context.Expenses.Where(e => e.UserId == userId).ToListAsync();

            return expenses;
        }

        public async Task<Expense> GetExpenseById(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            return expense;
        }

        public async Task<int> CreateExpense(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            return expense.Id;
        }

        public async Task DeleteExpense(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpense()
        {
            await _context.SaveChangesAsync();
        }
    }
}
