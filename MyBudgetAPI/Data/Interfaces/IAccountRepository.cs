using MyBudgetAPI.Dtos;
using MyBudgetAPI.Dtos.UserDto;
using MyBudgetAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task RegisterUserAsync(User user);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
