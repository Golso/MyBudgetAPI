using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Dtos;
using System.Threading.Tasks;

namespace MyBudgetApi.Services.Abstractions
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        Task<PagedList<UserReadDto>> GetAllUsersAsync(AccountParameters accountParameters);
        Task<string> GenerateJwt(LoginDto dto);
    }
}
