using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public interface IProfitRepository
    {
        Task<IEnumerable<ProfitReadDto>> GetAllProfits();
        Task<ProfitReadDto> GetProfitById(int id);
        Task<int> CreateProfit(ProfitCreateDto profitCreateDto);
        Task DeleteProfit(int id);
        Task UpdateProfit(int id, ProfitUpdateDto profitUpdateDto);
        Task PartialUpdateProfit(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument);
    }
}
