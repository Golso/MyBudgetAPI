using MyBudgetApi.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data
{
    public interface IProfitRepository
    {
        Task<IEnumerable<Profit>> GetAllProfitsAsync(int userId);
        Task<Profit> GetProfitByIdAsync(int id);
        Task<int> CreateProfitAsync(Profit profit);
        Task DeleteProfitAsync(Profit profit);
        Task SaveChangesAsync();
    }
}
