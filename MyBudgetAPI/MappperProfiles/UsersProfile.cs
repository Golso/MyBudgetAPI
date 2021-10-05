using AutoMapper;
using MyBudgetAPI.Dtos.UserDto;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
