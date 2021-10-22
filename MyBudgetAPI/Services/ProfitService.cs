using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MyBudgetAPI.Data;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Exceptions;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetAPI.Services
{
    public class ProfitService : IProfitService
    {
        private readonly IProfitRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ProfitService(IProfitRepository repository, IMapper mapper, IUserContextService userContextService)
        {
            _repository = repository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<int> CreateProfitAsync(ProfitCreateDto profitCreateDto)
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
            profit.UserId = _userContextService.GetUserId;

            return await _repository.CreateProfitAsync(profit);
        }

        public async Task DeleteProfitAsync(int id)
        {
            var profit = await _repository.GetProfitByIdAsync(id);

            if (profit is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            if (profit.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Profit of other user.");
            }

            await _repository.DeleteProfitAsync(profit);
        }

        public async Task<IEnumerable<ProfitReadDto>> GetAllProfitsAsync()
        {
            var userId = _userContextService.GetUserId;
            var profits = await _repository.GetAllProfitsAsync(userId);

            return _mapper.Map<List<ProfitReadDto>>(profits);
        }

        public async Task<ProfitReadDto> GetProfitByIdAsync(int id)
        {
            var profit = await _repository.GetProfitByIdAsync(id);

            if (profit is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            if (profit.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Profit of other user.");
            }

            return _mapper.Map<ProfitReadDto>(profit);
        }

        public async Task PartialUpdateProfitAsync(int id, JsonPatchDocument<ProfitUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                throw new BadRequestException("patchDocument object is null.");
            }

            var profitModelFromRepo = await _repository.GetProfitByIdAsync(id);

            if (profitModelFromRepo is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            if (profitModelFromRepo.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Profit of other user.");
            }

            var profitToPatch = _mapper.Map<ProfitUpdateDto>(profitModelFromRepo);
            patchDocument.ApplyTo(profitToPatch);

            _mapper.Map(profitToPatch, profitModelFromRepo);

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProfitAsync(int id, ProfitUpdateDto profitUpdateDto)
        {
            if (profitUpdateDto.Amount <= 0)
            {
                throw new BadRequestException("Amount is required and it should be positive number.");
            }

            var profit = await _repository.GetProfitByIdAsync(id);

            if (profit is null)
            {
                throw new NotFoundException("Profit not found.");
            }

            if (profit.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Profit of other user.");
            }

            profit.Amount = profitUpdateDto.Amount;
            profit.Source = profitUpdateDto.Source;
            profit.Description = profitUpdateDto.Description;

            await _repository.SaveChangesAsync();
        }
    }
}
