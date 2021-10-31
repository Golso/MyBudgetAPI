using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApi.Data
{
    public class DataSeeder
    {
        private readonly BudgetDbContext _dbContext;

        public DataSeeder(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                //if (!_dbContext.Expenses.Any())
                //{
                //    var expenses = GetExpenses();
                //    _dbContext.Expenses.AddRange(expenses);
                //    _dbContext.SaveChanges();
                //}


                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }

        //private IEnumerable<Expense> GetExpenses()
        //{
        //    var expenses = new List<Expense>() {
        //        new Expense()
        //        {
        //            Amount = 15,
        //            Category = "Food",
        //            Description = "Shopping etc.",
        //            Date = new DateTime(2021, 07, 29)
        //        },
        //        new Expense()
        //        {
        //            Amount = 1000,
        //            Category = "electronics",
        //            Description = "New mobile phone",
        //            Date = new DateTime(2021, 09, 08)
        //        }
        //    };

        //    return expenses;
        //}


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
