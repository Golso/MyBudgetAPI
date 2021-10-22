using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Controllers
{
    [Route("api/profits")]
    [ApiController]
    [Authorize]
    public class ProfitController : ControllerBase
    {
        private readonly IProfitService _service;

        public ProfitController(IProfitService service)
        {
            _service = service;
        }

        //GET /api/profits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfitReadDto>>> GetProfits()
        {
            var profits = await _service.GetAllProfitsAsync();

            return Ok(profits);
        }

        //GET /api/profits/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfitReadDto>> GetProfitById([FromRoute] int id)
        {
            var profit = await _service.GetProfitByIdAsync(id);

            return Ok(profit);
        }

        //PUT /api/profits
        [HttpPost]
        public async Task<ActionResult> CreateProfit([FromBody] ProfitCreateDto profitCreateDto)
        {
            var id = await _service.CreateProfitAsync(profitCreateDto);

            return Created($"/api/profits/{id}", null);
        }

        //DELETE /api/profits/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProfit([FromRoute] int id)
        {
            await _service.DeleteProfitAsync(id);

            return NoContent();
        }

        //PUT /api/profit/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProfit([FromRoute] int id, [FromBody] ProfitUpdateDto profitUpdateDto)
        {
            await _service.UpdateProfitAsync(id, profitUpdateDto);

            return NoContent();
        }


        //PATCH /api/profit/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialProfitUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<ProfitUpdateDto> patchDocument)
        {
            await _service.PartialUpdateProfitAsync(id, patchDocument);

            return NoContent();
        }
    }
}
