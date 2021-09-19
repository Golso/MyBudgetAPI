using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountController(IAccountRepository reposiory)
        {
            _repository = reposiory;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _repository.RegisterUser(dto);

            return Ok();
        }
    }
}
