using Microsoft.AspNetCore.JsonPatch;
using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data.Dtos;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IProfitService
    {
        Task<PagedList<ProfitReadDto>> GetAllProfitsAsync(ProfitParameters profitParameters);
        Task<ProfitReadDto> GetProfitByIdAsync(int id);
        Task<int> CreateProfitAsync(ProfitCreateDto profitCreateDto);
        Task DeleteProfitAsync(int id);
        Task UpdateProfitAsync(int id, ProfitUpdateDto profitUpdateDto);
        Task PartialUpdateProfitAsync(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument);

    }
}
