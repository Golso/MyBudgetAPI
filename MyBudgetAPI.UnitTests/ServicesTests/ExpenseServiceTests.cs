using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MyBudgetApi.Data;
using AutoMapper;
using MyBudgetApi.Services.Abstractions;
using MyBudgetApi.Services;
using MyBudgetApi.Data.Models;
using MyBudgetApi.MappperProfiles;
using MyBudgetApi.Data.Exceptions;
using FluentAssertions;
using MyBudgetApi.Data.Dtos;

namespace MyBudgetAPI.UnitTests.ServicesTests
{
    public class ExpenseServiceTests
    {
        private readonly Mock<IExpenseRepository> expenseRepoMock = new();
        private readonly Mock<IUserContextService> userContextServiceMock = new();
        private readonly IMapper _mapper;

        public ExpenseServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ExpensesProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task GetExpenseByIdAsync_WithExistingExpense_ShouldReturnExpense()
        {
            //Arrange
            Expense expense = new()
            {
                Id = 1,
                Amount = 123,
                Category = "Debit",
                Description = "Shopping",
                Date = DateTime.Parse("1999-01-02"),
                UserId = 1
            };
            expenseRepoMock.Setup(repo => repo.GetExpenseByIdAsync(1))
                .ReturnsAsync(expense);
            userContextServiceMock.Setup(userService => userService.GetUserId).Returns(1);
            ExpenseService expenseService = new(expenseRepoMock.Object, _mapper, userContextServiceMock.Object);
            

            //Act
            var result = await expenseService.GetExpenseByIdAsync(1);

            //Assert
            result.Should().BeEquivalentTo(_mapper.Map<ExpenseReadDto>(expense));
        }

        [Fact]
        public async Task GetExpenseByIdAsync_WithNotExistingExpense_ShouldReturnNotFoundException()
        {
            ///Arrange
            expenseRepoMock.Setup(repo => repo.GetExpenseByIdAsync(1))
                .ReturnsAsync((Expense)null);
            ExpenseService expenseService = new(expenseRepoMock.Object, _mapper, userContextServiceMock.Object);

            //Act
            Func<Task> func = async () => await expenseService.GetExpenseByIdAsync(1);

            //Assert
            await func.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Expense not found.");
        }

        [Fact]
        public async Task GetExpenseByIdAsync_WithWrongUserId_ShouldReturnForbiddenException()
        {
            ///Arrange
            Expense expense = new()
            {
                Id = 1,
                Amount = 123,
                Category = "Debit",
                Description = "Shopping",
                Date = DateTime.Parse("1999-01-02"),
                UserId = 2
            };
            expenseRepoMock.Setup(repo => repo.GetExpenseByIdAsync(1))
                .ReturnsAsync(expense);
            userContextServiceMock.Setup(userService => userService.GetUserId).Returns(1);
            ExpenseService expenseService = new(expenseRepoMock.Object, _mapper, userContextServiceMock.Object);


            //Act
            Func<Task> func = async () => await expenseService.GetExpenseByIdAsync(1);

            //Assert
            await func.Should()
                .ThrowAsync<ForbiddenException>()
                .WithMessage("Expense of other user.");
        }

        [Fact]
        public void CreateExpenseAsync_WithExpenseWithNoPositiveAmount_ShouldReturnBadRequestException()
        {
            //Arrange
            ExpenseCreateDto expenseCreateDto = new()
            {
                Amount = 0,
                Category = "Sth",
                Description = "Sth too",
                Date = DateTime.Parse("1999-11-11")
            };
            ExpenseService expenseService = new(expenseRepoMock.Object, _mapper, userContextServiceMock.Object);

            //Act
            Func<Task<int>> func = async () => await expenseService.CreateExpenseAsync(expenseCreateDto);

            //Assert
            func.Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Amount is required and it should be positive number.");
        }

        [Fact]
        public void CreateExpenseAsync_WithExpenseWithMinimalValueDatetime_ShouldReturnBadRequestException()
        {
            //Arrange
            ExpenseCreateDto expenseCreateDto = new()
            {
                Amount = 2,
                Category = "Sth",
                Description = "Sth too",
                Date = DateTime.MinValue
            };
            ExpenseService expenseService = new(expenseRepoMock.Object, _mapper, userContextServiceMock.Object);

            //Act
            Func<Task<int>> func = async () => await expenseService.CreateExpenseAsync(expenseCreateDto);

            //Assert
            func.Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Date is required.");
        }

        [Fact]
        public async Task CreateExpenseAsync_WithExpenseWithGoodValues_ShouldReturnId()
        {
            //Arrange
            ExpenseCreateDto dto = new()
            {
                Amount = 2,
                Category = "Sth",
                Description = "Sth too",
                Date = DateTime.Parse("2000-01-01")
            };
            expenseRepoMock.Setup(repo => repo.CreateExpenseAsync(It.IsAny<Expense>())).ReturnsAsync(1);
            userContextServiceMock.Setup(userContext => userContext.GetUserId).Returns(1);
            ExpenseService expenseService = new(expenseRepoMock.Object, _mapper, userContextServiceMock.Object);

            //Act
            var result = await expenseService.CreateExpenseAsync(dto);

            //Assert
            result.Should().Be(1);
        }
    }
}
