using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Exceptions;
using MyBudgetApi.Services.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Controllers
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
        public async Task<ActionResult<IEnumerable<ExpenseReadDto>>> GetAllExpensesAsync([FromQuery] ExpenseParameters expenseParameters)
        {
            if (!expenseParameters.ValidAmountRange)
            {
                throw new BadRequestException("Max amount cannot be less than min amount.");
            }
            if (!expenseParameters.ValidDateRange)
            {
                throw new BadRequestException("Max date cannot be less than min date.");
            }

            var expenses = await _service.GetAllExpensesAsync(expenseParameters);

            var metadata = new
            {
                expenses.TotalCount,
                expenses.PageSize,
                expenses.CurrentPage,
                expenses.TotalPages,
                expenses.HasNext,
                expenses.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(expenses.Items);
        }

        //GET /api/expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseReadDto>> GetExpenseByIdAsync([FromRoute] int id)
        {
            var expense = await _service.GetExpenseByIdAsync(id);

            return Ok(expense);
        }

        //POST /api/expenses
        [HttpPost]
        public async Task<ActionResult> CreateExpenseAsync([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            var id = await _service.CreateExpenseAsync(expenseCreateDto);

            return Created($"/api/expenses/{id}", null);
        }

        //DELETE /api/expense/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpenseAsync([FromRoute] int id)
        {
            await _service.DeleteExpenseAsync(id);

            return NoContent();
        }

        //PUT /api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpenseAsync([FromRoute] int id, [FromBody] ExpenseUpdateDto expenseUpdateDto)
        {
            await _service.UpdateExpenseAsync(id, expenseUpdateDto);

            return NoContent();
        }


        //PATCH /api/expenses/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialExpenseUpdateAsync([FromRoute] int id, [FromBody] JsonPatchDocument<ExpenseUpdateDto> patchDocument)
        {
            await _service.PartialUpdateExpenseAsync(id, patchDocument);

            return NoContent();
        }
    }
}
