using System.Collections.Generic;

namespace MoneyFlow.Models.DetailComponents
{
    public class ButtonFilter
    {
        public List<string> FilterList { get; set; }

        public List<string> ChosenFilters { get; set; }

        public ButtonFilter(List<string> filterList, List<string> chosenFilters)
        {
            FilterList = filterList;
            ChosenFilters = chosenFilters;
        }
    }

    public class ButtonSorting
    {
        public string SortingId { get; set; }

        public string ChoosenSorting { get; set; }

        public ButtonSorting(string sortingId, string choosenSorting)
        {
            SortingId = sortingId;
            ChoosenSorting = choosenSorting;
        }
    }
}