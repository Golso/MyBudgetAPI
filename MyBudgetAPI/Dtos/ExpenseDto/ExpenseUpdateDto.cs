﻿using System.ComponentModel.DataAnnotations;

namespace MyBudgetAPI.Dtos
{
    public class ExpenseUpdateDto
    {
        [Required]
        public double Amount { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        public string Description { get; set; }
    }
}
