namespace MoneyFlow.Models.DetailComponents
{
    public class SearchBar
    {
        public string Keyword { get; set; }
        
        public string CreateDataUrl { get; set; } = "";

        public ButtonFilter FilterButton { get; set; }

        public bool IsFilterable { get; set; } = false;

        public SearchBar(string keyword)
        {
            Keyword = keyword;
        }

        public SearchBar(string keyword, ButtonFilter filterButton)
        {
            Keyword = keyword;
            IsFilterable = true;
            FilterButton = filterButton;
        }
    }
}