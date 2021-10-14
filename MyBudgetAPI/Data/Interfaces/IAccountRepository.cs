using MyBudgetAPI.Dtos;
using MyBudgetAPI.Dtos.UserDto;
using System.Collections.Generic;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IAccountRepository
    {
        public void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        IEnumerable<UserReadDto> GetAllUsers();
    }
}
