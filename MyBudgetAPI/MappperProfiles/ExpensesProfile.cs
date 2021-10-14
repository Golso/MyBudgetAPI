using AutoMapper;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;

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
            CreateMap<Expense, ExpenseUpdateDto>();
            CreateMap<ExpenseUpdateDto, Expense>();
        }
    }
}
