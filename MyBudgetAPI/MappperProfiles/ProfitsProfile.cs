using AutoMapper;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;

namespace MyBudgetAPI.MappperProfiles
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
