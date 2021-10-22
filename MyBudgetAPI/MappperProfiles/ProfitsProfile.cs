using AutoMapper;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Models;

namespace MyBudgetApi.MappperProfiles
{
    public class ProfitsProfile : Profile
    {
        public ProfitsProfile()
        {
            CreateMap<Profit, ProfitReadDto>();
            CreateMap<ProfitCreateDto, Profit>();
            CreateMap<ProfitReadDto, Profit>();
            CreateMap<Profit, ProfitUpdateDto>();
            CreateMap<ProfitUpdateDto, Profit>();
        }
    }
}
