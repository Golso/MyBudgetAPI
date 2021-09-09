using AutoMapper;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.MappperProfiles
{
    public class ExpensesProfile : Profile
    {
        public ExpensesProfile()
        {
            //Source -> Target
            CreateMap<Expense, ExpenseReadDto>();
            CreateMap<ExpenseReadDto, Expense>();
            CreateMap<ExpenseCreateDto, Expense>();
        }
    }
}
