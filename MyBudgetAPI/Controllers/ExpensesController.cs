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

        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetAllExpenses()
        {
            var expenses = _repository.GetAllExpenses();

            return Ok();
        }
    }
}
