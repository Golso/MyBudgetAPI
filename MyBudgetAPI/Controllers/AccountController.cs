using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Dtos.UserDto;
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

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _repository.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _repository.GetAllUsers();

            return Ok(users);
        }
    }
}
