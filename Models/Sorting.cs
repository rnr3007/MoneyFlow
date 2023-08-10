using od = MoneyFlow.Constants.OrderConstants;

namespace MoneyFlow.Models
{
    public class Sorting
    {
        public string ActionUrl { get; set; }

        public string SortingId { get; set; }

        public string ChoosenSorting { get; set; }

        public Sorting(string actionUrl, string sortingId, string choosenSorting)
        {
            ActionUrl = actionUrl;
            SortingId = sortingId;
            ChoosenSorting = choosenSorting;
        }
    }
}