using MyBudgetApi.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IAccountRepository
    {
        Task RegisterUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
