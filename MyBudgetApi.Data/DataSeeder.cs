using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApi.Data
{
    public class DataSeeder
    {
        private readonly BudgetDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;


        public DataSeeder(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new();
        }

        public void Seed()
        {
            _dbContext.Database.Migrate();

            if (_dbContext.Database.CanConnect())
            {
                
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.Add(users[0]);
                    _dbContext.SaveChanges();
                    _dbContext.Users.Add(users[1]);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Expenses.Any())
                {
                    var expenses = GetExpenses();
                    _dbContext.Expenses.AddRange(expenses);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Expense> GetExpenses()
        {
            var expenses = new List<Expense>()
            {
                new Expense()
                {
                    Amount = 99,
                    Category = "Shopping",
                    Description = "Food store",
                    Date = DateTime.Parse("2012-03-01"),
                    UserId = 2
                },
                new Expense()
                {
                    Amount = 1099.99,
                    Category = "Electronics",
                    Description = "Mobile phone",
                    Date = DateTime.Parse("2014-02-01"),
                    UserId = 2
                },
                new Expense()
                {
                    Amount = 19.99,
                    Category = "Shopping",
                    Description = "Cigaretes",
                    Date = DateTime.Parse("2013-01-01"),
                    UserId = 2
                },
                new Expense()
                {
                    Amount = 99,
                    Category = "Test",
                    Description = "Admin expense",
                    Date = DateTime.Parse("2021-01-01"),
                    UserId = 1
                }
            };

            return expenses;
        }

        private List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    RoleId = 2
                },
                new User()
                {
                    Email = "user@gmail.com",
                    FirstName = "user",
                    LastName = "user",
                    RoleId = 1
                }
            };
            var adminHashedPassword = _passwordHasher.HashPassword(users[0], "admin");
            users[0].PasswordHash = adminHashedPassword;

            var userHashedPassword = _passwordHasher.HashPassword(users[1], "user");
            users[1].PasswordHash = userHashedPassword;

            return users;
        }


        private static IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
    }
}
