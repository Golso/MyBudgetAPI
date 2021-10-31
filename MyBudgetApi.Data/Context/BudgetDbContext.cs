using Microsoft.EntityFrameworkCore;
using MyBudgetApi.Data.Models;

namespace MyBudgetApi.Data.Context
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> opt) : base(opt)
        {

        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
