using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudgetApi.Data.Dtos
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
