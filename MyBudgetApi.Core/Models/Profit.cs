using System;
using System.ComponentModel.DataAnnotations;

namespace MyBudgetApi.Data.Models
{
    public class Profit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [MaxLength(50)]
        public string Source { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
