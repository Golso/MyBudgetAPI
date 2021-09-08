using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
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
        public ActionResult<IEnumerable<Expense>> GetAllExpenses()
        {
            var expenses = _repository.GetAllExpenses();

            return Ok(expenses);
        }

        //GET /api/expenses/{id}
        [HttpGet("{id}")]
        public ActionResult<Expense> GetExpenseById([FromRoute] int id)
        {
            var expense = _repository.GetExpenseById(id);

            if (expense is null)
            {
                return NotFound();
            }

            return Ok(expense);
        }
    }
}
