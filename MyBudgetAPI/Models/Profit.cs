using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Models
{
    public class Profit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        public string Source { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
