using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class SummaryViewModel
    {
        public IEnumerable<dynamic> TotalCostByDate { get; set; }

        public List<Motivation> MotivationList { get; set; }

        public long Savings { get; set; }
    }
}