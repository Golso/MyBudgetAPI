using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
using MyBudgetAPI.Models;
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

        public int CreateProfit(ProfitCreateDto profitCreateDto)
        {
            if (profitCreateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }
            if (profitCreateDto.Date == DateTime.MinValue)
            {
                throw new BadRequestException("Date is required.");
            }

            var profit = _mapper.Map<Profit>(profitCreateDto);
            _context.Profits.Add(profit);
            _context.SaveChanges();

            return profit.Id;
        }

        public void DeleteProfit(int id)
        {
            var profit = _context.Profits.Find(id);

            if (profit is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            _context.Profits.Remove(profit);
            _context.SaveChanges();
        }

        public IEnumerable<ProfitReadDto> GetAllProfits()
        {
            var profits = _context.Profits.ToList();

            return _mapper.Map<List<ProfitReadDto>>(profits);
        }

        public ProfitReadDto GetProfitById(int id)
        {
            var profit = _context.Profits.Find(id);

            if(profit is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            return _mapper.Map<ProfitReadDto>(profit);
        }

        public void PartialUpdateProfit(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                throw new BadRequestException("patchDocument object is null.");
            }

            var profitModelFromRepo = _context.Profits.Find(id);

            if (profitModelFromRepo is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            var profitToPatch = _mapper.Map<ProfitUpdateDto>(profitModelFromRepo);
            patchDocument.ApplyTo(profitToPatch);

            _mapper.Map(profitToPatch, profitModelFromRepo);

            _context.SaveChanges();
        }

        public void UpdateProfit(int id, ProfitUpdateDto profitUpdateDto)
        {
            if (profitUpdateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }

            var profit = _context.Profits.Find(id);

            if (profit is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            profit.Amount = profitUpdateDto.Amount;
            profit.Source = profitUpdateDto.Source;
            profit.Description = profitUpdateDto.Description;

            _context.SaveChanges();
        }
    }
}
