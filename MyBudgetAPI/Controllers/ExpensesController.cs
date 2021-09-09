using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IBudgetRepo _repository;
        

        public ExpensesController(IBudgetRepo reposiory)
        {
            _repository = reposiory;
        }

        //GET /api/expenses
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseReadDto>> GetAllExpenses()
        {
            var expenses = _repository.GetAllExpenses();

            return Ok(expenses);
        }

        //GET /api/expenses/{id}
        [HttpGet("{id}")]
        public ActionResult<ExpenseReadDto> GetExpenseById([FromRoute] int id)
        {
            var expense = _repository.GetExpenseById(id);

            if (expense is null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        //POST /api/expenses
        [HttpPost]
        public ActionResult CreateExpense([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var id = _repository.CreateExpense(expenseCreateDto);

            return Created($"/api/expenses/{id}", null);
        }

        //DELETE /api/expense/{id}
        [HttpDelete("{id}")]
        public ActionResult<Expense> DeleteExpense(int id)
        {
            var expense = _repository.DeleteExpense(id);

            if (expense is null)
            {
                return NotFound();
            }

            return Ok(expense);
        }
    }
}
