using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBudgetApi.Controllers;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Services.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyBudgetAPI.UnitTests.ControllerTests
{
    public class ExpensesControllerTests
    {
        private readonly Mock<IExpenseService> serviceMock = new();

        [Fact]
        public async Task GetExpenseByIdAsync_WithExistingItem_ReturnsOkObjectResult()
        {
            //Arrange
            ExpenseReadDto dto = new()
            {
                Id = 1,
                Amount = 213,
                Category = "Debit",
                Description = "Shopping",
                Date = DateTime.Parse("1999-03-02")
            };

            serviceMock.Setup(service => service.GetExpenseByIdAsync(1))
                .ReturnsAsync(dto);

            var controller = new ExpensesController(serviceMock.Object);

            //Act
            ActionResult<ExpenseReadDto> result = await controller.GetExpenseByIdAsync(1);

            //Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            ExpenseReadDto readExpense = Assert.IsType<ExpenseReadDto>(objectResult.Value);
            Assert.NotNull(readExpense);
        }
    }
}
