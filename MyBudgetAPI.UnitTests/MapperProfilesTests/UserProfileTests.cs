using AutoMapper;
using FluentAssertions;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Models;
using MyBudgetApi.MappperProfiles;
using System;
using Xunit;

namespace MyBudgetAPI.UnitTests.MapperProfilesTests
{
    public class UserProfileTests
    {
        private readonly IMapper _mapper;

        public UserProfileTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new UsersProfile());
                });

                _mapper = mappingConfig.CreateMapper();
            }
        }

        [Fact]
        public void MappingUserToUserDto_CorrespondingPropertiesShouldBeEqual()
        {
            // Arrange
            int id = 1;
            string email = "mail@gm.com";
            string firstName = "Marcus";
            string lastName = "Aurelius";
            DateTime dateOfBirth = DateTime.Parse("1999-02-03");
            string passwordHash = "12312";
            int roleId = 1;

            User user = new()
            {
                Id = id,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                PasswordHash = passwordHash,
                RoleId = roleId
            };

            UserReadDto userDto = new()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            //Act
            UserReadDto dto = _mapper.Map<UserReadDto>(user);

            //Assert

            dto.Should().BeEquivalentTo(userDto, options => options.ComparingByMembers<UserReadDto>());
        }
    }
}
