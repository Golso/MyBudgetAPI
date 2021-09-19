using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Dtos
{
    public class ExpenseCreateDto
    {
        [Required]
        public double Amount { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
