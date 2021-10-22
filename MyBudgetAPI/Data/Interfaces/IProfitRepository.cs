using MyBudgetAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
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
