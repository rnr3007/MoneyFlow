using System.Collections.Generic;

namespace MoneyFlow.Models.DetailComponents
{
    public class ButtonFilter
    {
        public List<int> FilterList { get; set; }

        public List<int> ChosenFilters { get; set; }

        public ButtonFilter(List<int> filterList, List<int> chosenFilters)
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