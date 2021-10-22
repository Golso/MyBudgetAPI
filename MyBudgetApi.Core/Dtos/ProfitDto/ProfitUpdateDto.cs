using System.ComponentModel.DataAnnotations;

namespace MyBudgetApi.Data.Dtos
{
    public class ProfitUpdateDto
    {
        [Required]
        public double Amount { get; set; }

        [MaxLength(50)]
        public string Source { get; set; }

        public string Description { get; set; }
    }
}
