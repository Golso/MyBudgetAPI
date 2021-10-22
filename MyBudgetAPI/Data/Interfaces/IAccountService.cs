using MyBudgetAPI.Dtos;
using MyBudgetAPI.Dtos.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<string> GenerateJwt(LoginDto dto);
    }
}
