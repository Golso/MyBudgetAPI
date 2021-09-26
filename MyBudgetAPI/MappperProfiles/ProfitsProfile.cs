﻿using AutoMapper;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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