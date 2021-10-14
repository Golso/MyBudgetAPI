using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudgetAPI.Dtos
{
    public class ProfitCreateDto
    {
        [Required]
        public double Amount { get; set; }

        [MaxLength(50)]
        public string Source { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
