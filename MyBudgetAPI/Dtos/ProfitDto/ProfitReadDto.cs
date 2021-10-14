using System;

namespace MyBudgetAPI.Dtos
{
    public class ProfitReadDto
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public string Source { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
