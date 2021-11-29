using AutoMapper;
using FluentAssertions;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Models;
using MyBudgetApi.MappperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBudgetAPI.UnitTests.MapperProfilesTests
{
    public class ExpensesProfileTests
    {
        private readonly IMapper _mapper;

        public ExpensesProfileTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ExpensesProfile());
                });

                _mapper = mappingConfig.CreateMapper();
            }
        }

        [Fact]
        public void MappingExpenseToExpenseReadDto_CorrespondingPropertiesShouldBeEqual()
        {
            // Arrange
            int id = 1;
            double amount = 123.2;
            string category = "cat";
            string description = "shopping";
            DateTime date = DateTime.Parse("2000-01-01");
            int userId = 1;
            Expense expense = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date,
                UserId = userId
            };

            ExpenseReadDto expenseReadDto = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date
            };

            // Act
            ExpenseReadDto dto = _mapper.Map<ExpenseReadDto>(expense);

            // Assert
            dto.Should().BeEquivalentTo(expenseReadDto, options => options.ComparingByMembers<ExpenseReadDto>());
        }

        [Fact]
        public void MappingExpenseToExpenseUpdateDto_CorrespondingPropertiesShouldBeEqual()
        {
            // Arrange
            int id = 1;
            double amount = 123.2;
            string category = "cat";
            string description = "shopping";
            DateTime date = DateTime.Parse("2000-01-01");
            int userId = 1;
            Expense expense = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date,
                UserId = userId
            };

            ExpenseUpdateDto expenseUpdateDto = new()
            {
                Amount = amount,
                Category = category,
                Description = description
            };

            // Act
            ExpenseUpdateDto dto = _mapper.Map<ExpenseUpdateDto>(expense);

            // Assert
            dto.Should().BeEquivalentTo(expenseUpdateDto, options => options.ComparingByMembers<ExpenseUpdateDto>());
        }

        [Fact]
        public void MappingExpenseReadDtoToExpense_CorrespondingPropertiesShouldBeEqual()
        {
            // Arrange
            int id = 1;
            double amount = 123.2;
            string category = "cat";
            string description = "shopping";
            DateTime date = DateTime.Parse("2000-01-01");
            int userId = 1;
            Expense expense = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date,
                UserId = userId
            };

            ExpenseReadDto expenseReadDto = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date
            };

            // Act
            Expense dto = _mapper.Map<Expense>(expenseReadDto);
            dto.UserId = userId;

            // Assert
            dto.Should().BeEquivalentTo(expense, options => options.ComparingByMembers<Expense>());
        }

        [Fact]
        public void MappingExpenseCreateDtoToExpense_CorrespondingPropertiesShouldBeEqual()
        {
            // Arrange
            int id = 1;
            double amount = 123.2;
            string category = "cat";
            string description = "shopping";
            DateTime date = DateTime.Parse("2000-01-01");
            int userId = 1;
            Expense expense = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date,
                UserId = userId
            };

            ExpenseCreateDto expenseCreateDto = new()
            {
                Amount = amount,
                Category = category,
                Description = description,
                Date = date
            };

            // Act
            Expense dto = _mapper.Map<Expense>(expenseCreateDto);
            dto.Id = id;
            dto.UserId = userId;

            // Assert
            dto.Should().BeEquivalentTo(expense, options => options.ComparingByMembers<Expense>());
        }

        [Fact]
        public void MappingExpenseUpdateDtoToExpense_CorrespondingPropertiesShouldBeEqual()
        {
            // Arrange
            int id = 1;
            double amount = 123.2;
            string category = "cat";
            string description = "shopping";
            DateTime date = DateTime.Parse("2000-01-01");
            int userId = 1;
            Expense expense = new()
            {
                Id = id,
                Amount = amount,
                Category = category,
                Description = description,
                Date = date,
                UserId = userId
            };

            ExpenseUpdateDto updateCreateDto = new()
            {
                Amount = amount,
                Category = category,
                Description = description
            };

            // Act
            Expense dto = _mapper.Map<Expense>(updateCreateDto);
            dto.Id = id;
            dto.Date = date;
            dto.UserId = userId;

            // Assert
            dto.Should().BeEquivalentTo(expense, options => options.ComparingByMembers<Expense>());
        }
    }
}
