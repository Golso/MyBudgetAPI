using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class ProfitRepository : IProfitRepository
    {
        private readonly BudgetDbContext _context;
        private readonly IMapper _mapper;

        public ProfitRepository(BudgetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int CreateProfit(ProfitCreateDto expense)
        {
            throw new NotImplementedException();
        }

        public void DeleteProfit(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProfitReadDto> GetAllProfits()
        {
            var profits = _context.Profits.ToList();

            return _mapper.Map<List<ProfitReadDto>>(profits);
        }

        public ProfitReadDto GetProfitById(int id)
        {
            throw new NotImplementedException();
        }

        public void PartialUpdateProfit(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfit(int id, ProfitUpdateDto expenseUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
