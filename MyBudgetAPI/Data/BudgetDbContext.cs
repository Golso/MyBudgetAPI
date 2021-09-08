using Microsoft.EntityFrameworkCore;
using MyBudgetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> opt) : base(opt)
        {

        } 

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Profit> Profits { get; set; }
    }
}
