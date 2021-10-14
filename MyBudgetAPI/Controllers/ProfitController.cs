using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;

namespace MyBudgetAPI.Controllers
{
    [Route("api/profits")]
    [ApiController]
    [Authorize]
    public class ProfitController : ControllerBase
    {
        private readonly IProfitRepository _repository;

        public ProfitController(IProfitRepository repository)
        {
            _repository = repository;
        }

        //GET /api/profits
        [HttpGet]
        public ActionResult<IEnumerable<ProfitReadDto>> GetProfits()
        {
            var profits = _repository.GetAllProfits();

            return Ok(profits);
        }

        //GET /api/profits/{id}
        [HttpGet("{id}")]
        public ActionResult<ProfitReadDto> GetProfitById([FromRoute] int id)
        {
            var profit = _repository.GetProfitById(id);

            return Ok(profit);
        }

        //PUT /api/profits
        [HttpPost]
        public ActionResult CreateProfit([FromBody] ProfitCreateDto profitCreateDto)
        {
            var id = _repository.CreateProfit(profitCreateDto);

            return Created($"/api/profits/{id}", null);
        }

        //DELETE /api/profits/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProfit([FromRoute] int id)
        {
            _repository.DeleteProfit(id);

            return NoContent();
        }

        //PUT /api/profit/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProfit([FromRoute] int id, [FromBody] ProfitUpdateDto profitUpdateDto)
        {
            _repository.UpdateProfit(id, profitUpdateDto);

            return NoContent();
        }


        //PATCH /api/profit/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialProfitUpdate([FromRoute] int id, [FromBody] JsonPatchDocument<ProfitUpdateDto> patchDocument)
        {
            _repository.PartialUpdateProfit(id, patchDocument);

            return NoContent();
        }
    }
}
