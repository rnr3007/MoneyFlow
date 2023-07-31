using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class SummaryViewModel
    {
        public IEnumerable<dynamic> TotalCostByDate { get; set; }

        public IEnumerable<dynamic> TotalCostByType { get; set; }
    }
}