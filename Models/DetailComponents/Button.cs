using System.Collections.Generic;

namespace MoneyFlow.Models.DetailComponents
{
    public class ButtonFilter
    {
        public string DataEndpoint { get; set; }

        public List<string> FilterList { get; set; }

        public List<string> ChosenFilters { get; set; }

        public ButtonFilter(string dataEndpoint, List<string> filterList, List<string> chosenFilters)
        {
            DataEndpoint = dataEndpoint;
            FilterList = filterList;
            ChosenFilters = chosenFilters;
        }
    }

    public class ButtonSorting
    {
        public string ActionUrl { get; set; }

        public string SortingId { get; set; }

        public string ChoosenSorting { get; set; }

        public ButtonSorting(string actionUrl, string sortingId, string choosenSorting)
        {
            ActionUrl = actionUrl;
            SortingId = sortingId;
            ChoosenSorting = choosenSorting;
        }
    }
}