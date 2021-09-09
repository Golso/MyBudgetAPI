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
        private readonly IMapper _mapper;

        public ExpensesController(IBudgetRepo reposiory, IMapper mapper)
        {
            _repository = reposiory;
            _mapper = mapper;
        }

        //GET /api/expenses
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseReadDto>> GetAllExpenses()
        {
            var expenses = _repository.GetAllExpenses();

            return Ok(_mapper.Map<List<ExpenseReadDto>>(expenses));
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

            return Ok(_mapper.Map<ExpenseReadDto>(expense));
        }

        //POST /api/expenses
        [HttpPost]
        public ActionResult CreateExpense([FromBody] ExpenseCreateDto expenseCreateDto)
        {
            var expense = _mapper.Map<Expense>(expenseCreateDto);
            _repository.CreateExpense(expense);

            return Created($"/api/expenses/{expense.Id}", null);
        }
    }
}
