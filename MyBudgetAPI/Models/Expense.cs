using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
