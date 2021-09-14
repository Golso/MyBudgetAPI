using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetAPI.Dtos
{
    public class ProfitReadDto
    {
        public double Amount { get; set; }

        public string Source { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
