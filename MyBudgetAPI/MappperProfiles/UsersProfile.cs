using AutoMapper;
using MyBudgetAPI.Dtos.UserDto;
using MyBudgetAPI.Models;

namespace MyBudgetAPI.MappperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
