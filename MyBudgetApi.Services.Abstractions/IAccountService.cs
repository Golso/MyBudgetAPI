using MyBudgetApi.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<string> GenerateJwt(LoginDto dto);
    }
}
