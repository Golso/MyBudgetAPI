using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using MyBudgetApi.Core.Helpers;
using MyBudgetApi.Core.Models;
using MyBudgetApi.Data;
using MyBudgetApi.Data.Abstractions;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Exceptions;
using MyBudgetApi.Data.Models;
using MyBudgetApi.MappperProfiles;
using MyBudgetApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyBudgetAPI.UnitTests.ServicesTests
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> accountRepoMock = new();
        private readonly IMapper _mapper;
        private readonly Mock<IPasswordHasher<User>> passwordHasherMock = new();
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountServiceTests()
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
        public async Task GetAllUsersAsync_ShouldReturnPagedList()
        {
            //Arrange
            AccountParameters accountParameters = new();
            List<User> listOfUsers = new();
            PagedList<User> pagedListOfUsers = new(listOfUsers, 0, 1, 1);
            PagedList<UserReadDto> pagedListOfDto = new(_mapper.Map<List<UserReadDto>>(listOfUsers), 0, 1, 1);
            accountRepoMock.Setup(repo => repo.GetAllUsersAsync(accountParameters)).ReturnsAsync(pagedListOfUsers);
            AccountService accountService = new(accountRepoMock.Object, _mapper, passwordHasherMock.Object, _authenticationSettings);

            //Act
            PagedList<UserReadDto> result = await accountService.GetAllUsersAsync(accountParameters);

            //Assert
            result.Should().BeEquivalentTo(
                pagedListOfDto,
                options => options.ComparingByMembers<PagedList<UserReadDto>>());
        }

        [Fact]
        public async Task GenerateJwt_IfUserDontExists_ShouldThrowBadRequestException()
        {
            //Arrange
            LoginDto loginDto = new()
            {
                Email = "email@gma.com",
                Password = "password"
            };
            accountRepoMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);
            AccountService accountService = new(accountRepoMock.Object, _mapper, passwordHasherMock.Object, _authenticationSettings);

            //Act
            Func<Task> func = async () => await accountService.GenerateJwt(loginDto);

            //Assert
            await func.Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Invalid username or password");
        }

        [Fact]
        public async Task GenerateJwt_IfPasswordIsWrong_ShouldThrowBadRequestException()
        {
            //Arrange
            LoginDto loginDto = new()
            {
                Email = "email@gma.com",
                Password = "password"
            };
            User user = new()
            {
                Id = 1,
                Email = "email@gma.com",
                FirstName = "Mars",
                LastName = "Wenus",
                DateOfBirth = DateTime.Parse("1090-01-01"),
                PasswordHash = "hash",
                RoleId = 0
            };
            accountRepoMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            passwordHasherMock.Setup(hasher =>
                hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password))
                .Returns(PasswordVerificationResult.Failed);
            AccountService accountService = new(accountRepoMock.Object, _mapper, passwordHasherMock.Object, _authenticationSettings);

            //Act
            Func<Task> func = async () => await accountService.GenerateJwt(loginDto);

            //Assert
            await func.Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("Invalid username or password");
        }
    }
}
