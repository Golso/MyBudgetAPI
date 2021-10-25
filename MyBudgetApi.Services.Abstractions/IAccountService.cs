using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        Task<PagedList<UserReadDto>> GetAllUsersAsync(AccountParameters accountParameters);
        Task<string> GenerateJwt(LoginDto dto);
    }
}
