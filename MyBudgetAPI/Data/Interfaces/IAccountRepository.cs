using MyBudgetAPI.Dtos;
using MyBudgetAPI.Dtos.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IAccountRepository
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        Task<IEnumerable<UserReadDto>> GetAllUsers();
    }
}
