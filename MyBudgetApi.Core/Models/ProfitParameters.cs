using System;

namespace MyBudgetApi.Core.Models
{
    public class ProfitParameters : QueryStringParameters
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; } = DateTime.Now;
        public bool ValidDateRange => DateTime.Compare(MinDate, MaxDate) < 0;

        public uint MinAmount { get; set; }
        public uint MaxAmount { get; set; } = uint.MaxValue;
        public bool ValidAmountRange => MinAmount < MaxAmount;
    }
}
