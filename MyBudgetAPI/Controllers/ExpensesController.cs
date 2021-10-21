using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
using MyBudgetAPI.Data.Interfaces;
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
        private readonly IExpenseService _service;

        public ExpensesController(IExpenseService service)
        {
            _service = service;
        }

        //GET /api/expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseReadDto>>> GetAllExpenses()
        {
            var expenses = await _service.GetAllExpenses();

            return Ok(expenses);
        }

        //GET /api/expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseReadDto>> GetExpenseById([FromRoute] int id)
        {
            var expense = await _service.GetExpenseById(id);

            return Ok(expense);
        }

        //POST /api/expenses
        [HttpPost]
        public async Task<ActionResult> CreateExpense([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            var id = await _service.CreateExpense(expenseCreateDto);

            return Created($"/api/expenses/{id}", null);
        }

        //DELETE /api/expense/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense([FromRoute] int id)
        {
            await _service.DeleteExpense(id);

            return NoContent();
        }

        //PUT /api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense([FromRoute] int id, [FromBody] ExpenseUpdateDto expenseUpdateDto)
        {
            await _service.UpdateExpense(id, expenseUpdateDto);

            return NoContent();
        }


        //PATCH /api/expenses/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialExpenseUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            await _service.PartialUpdateExpense(id, patchDocument);

            return NoContent();
        }
    }
}
