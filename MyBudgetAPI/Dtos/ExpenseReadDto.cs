using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Dtos
{
    public class ExpenseReadDto
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
