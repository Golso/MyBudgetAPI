using AutoMapper;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Models;

namespace MyBudgetApi.MappperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
