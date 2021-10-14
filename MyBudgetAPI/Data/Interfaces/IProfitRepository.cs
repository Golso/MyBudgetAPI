using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using System.Collections.Generic;

namespace MyBudgetAPI.Data
{
    public interface IProfitRepository
    {
        IEnumerable<ProfitReadDto> GetAllProfits();
        ProfitReadDto GetProfitById(int id);
        int CreateProfit(ProfitCreateDto profitCreateDto);
        void DeleteProfit(int id);
        void UpdateProfit(int id, ProfitUpdateDto profitUpdateDto);
        void PartialUpdateProfit(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument);
    }
}
