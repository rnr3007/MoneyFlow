using System.Collections.Generic;
using MoneyFlow.Data;

namespace MoneyFlow.Models
{
    public class SummaryView
    {
        public IEnumerable<dynamic> TotalCostByDate { get; set; }

        public List<Motivation> MotivationList { get; set; }

        public long Savings { get; set; }
    }
}