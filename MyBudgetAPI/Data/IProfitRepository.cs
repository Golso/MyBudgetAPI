using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public interface IProfitRepository
    {
        IEnumerable<ProfitReadDto> GetAllProfits();
        ProfitReadDto GetProfitById(int id);
        int CreateProfit(ProfitCreateDto expense);
        void DeleteProfit(int id);
        void UpdateProfit(int id, ProfitUpdateDto expenseUpdateDto);
        void PartialUpdateProfit(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument);
    }
}
