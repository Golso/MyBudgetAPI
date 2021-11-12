using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Models;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IAccountRepository
    {
        Task RegisterUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<PagedList<User>> GetAllUsersAsync(AccountParameters accountParameters);
    }
}
