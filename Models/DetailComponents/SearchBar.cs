using MoneyFlow.Models.DetailComponents;

namespace MoneyFlow.Models.DetailComponents
{
    public class SearchBar
    {
        public string DataEndpoint { get; set; }

        public string Keyword { get; set; }
        
        public string CreateDataUrl { get; set; }

        public ButtonFilter FilterButton { get; set; }

        public bool IsFilterable { get; set; } = false;

        public SearchBar(string dataUrl, string keyword, string createDataUrl)
        {
            DataEndpoint = dataUrl;
            Keyword = keyword;
            CreateDataUrl = createDataUrl;
        }

        public SearchBar(string dataUrl, string keyword, string createDataUrl, ButtonFilter filterButton)
        {
            DataEndpoint = dataUrl;
            Keyword = keyword;
            CreateDataUrl = createDataUrl;
            IsFilterable = true;
            FilterButton = filterButton;
        }
    }
}