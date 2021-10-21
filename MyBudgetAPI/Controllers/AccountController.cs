using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Dtos.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountService _service;

        public AccountController(IAccountRepository reposiory, IAccountService service)
        {
            _repository = reposiory;
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _service.RegisterUserAsync(dto);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            string token = await _service.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();

            return Ok(users);
        }
    }
}
