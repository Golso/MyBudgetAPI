using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data;
using MyBudgetAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Controllers
{
    [Route("api/profits")]
    [ApiController]
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
    }
}
