using AutoMapper;
using MyBudgetAPI.Data.Interfaces;
using MyBudgetAPI.Dtos;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BudgetDbContext _context;

        public AccountRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
