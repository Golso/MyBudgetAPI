using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository _repository;

        public ExpensesController(IExpenseRepository repository)
        {
            _repository = repository;
        }

        //GET /api/expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseReadDto>>> GetAllExpenses()
        {
            var expenses = await _repository.GetAllExpenses();

            return Ok(expenses);
        }

        //GET /api/expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseReadDto>> GetExpenseById([FromRoute] int id)
        {
            var expense = await _repository.GetExpenseById(id);

            return Ok(expense);
        }

        //POST /api/expenses
        [HttpPost]
        public async Task<ActionResult> CreateExpense([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            var id = await _repository.CreateExpense(expenseCreateDto);

            return Created($"/api/expenses/{id}", null);
        }

        //DELETE /api/expense/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense([FromRoute] int id)
        {
            await _repository.DeleteExpense(id);

            return NoContent();
        }

        //PUT /api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense([FromRoute] int id, [FromBody] ExpenseUpdateDto expenseUpdateDto)
        {
            await _repository.UpdateExpense(id, expenseUpdateDto);

            return NoContent();
        }


        //PATCH /api/expenses/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialExpenseUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            await _repository.PartialUpdateExpense(id, patchDocument);

            return NoContent();
        }
    }
}
