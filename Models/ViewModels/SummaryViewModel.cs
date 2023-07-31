using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class SummaryViewModel
    {
        public List<long> TotalCostByDate { get; set; }

        public List<long> TotalCostByType { get; set; }
    }
}