using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
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
        private readonly IExpenseRepository _repository;
        
        public ExpensesController(IExpenseRepository reposiory)
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

            return Ok(expense);
        }

        //POST /api/expenses
        [HttpPost]
        public ActionResult CreateExpense([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            var id = _repository.CreateExpense(expenseCreateDto);

            return Created($"/api/expenses/{id}", null);
        }

        //DELETE /api/expense/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteExpense([FromRoute] int id)
        {
            _repository.DeleteExpense(id);

            return NoContent();
        }

        //PUT /api/expenses/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateExpense([FromRoute] int id, [FromBody] ExpenseUpdateDto expenseUpdateDto)
        {
            _repository.UpdateExpense(id, expenseUpdateDto);

            return Ok();
        }


        //PATCH /api/expenses/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialExpenseUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            _repository.PartialUpdateExpense(id, patchDocument);

            return Ok();
        }
    }
}
