using AutoMapper;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Models;

namespace MyBudgetApi.MappperProfiles
{
    public class ExpensesProfile : Profile
    {
        public ExpensesProfile()
        {
            //Source -> Target
            CreateMap<Expense, ExpenseReadDto>();
            CreateMap<ExpenseReadDto, Expense>();
            CreateMap<ExpenseCreateDto, Expense>();
            CreateMap<Expense, ExpenseUpdateDto>();
            CreateMap<ExpenseUpdateDto, Expense>();
        }
    }
}
