using Microsoft.AspNetCore.JsonPatch;
using MyBudgetApi.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApi.Data.Abstractions
{
    public interface IProfitService
    {
        Task<IEnumerable<ProfitReadDto>> GetAllProfitsAsync();
        Task<ProfitReadDto> GetProfitByIdAsync(int id);
        Task<int> CreateProfitAsync(ProfitCreateDto profitCreateDto);
        Task DeleteProfitAsync(int id);
        Task UpdateProfitAsync(int id, ProfitUpdateDto profitUpdateDto);
        Task PartialUpdateProfitAsync(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument);

    }
}
