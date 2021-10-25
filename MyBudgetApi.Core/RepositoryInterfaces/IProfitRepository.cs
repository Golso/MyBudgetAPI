using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Models;
using System.Threading.Tasks;

namespace MyBudgetApi.Data
{
    public interface IProfitRepository
    {
        Task<PagedList<Profit>> GetAllProfitsAsync(int userId, ProfitParameters profitParameters);
        Task<Profit> GetProfitByIdAsync(int id);
        Task<int> CreateProfitAsync(Profit profit);
        Task DeleteProfitAsync(Profit profit);
        Task SaveChangesAsync();
    }
}
