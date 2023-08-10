namespace MoneyFlow.Models
{
    public class SearchBarWithAddData
    {
        public string DataEndpoint { get; set; }

        public string Keyword { get; set; }
        
        public string CreateDataUrl { get; set; }

        public SearchBarWithAddData(string dataUrl, string keyword, string createDataUrl)
        {
            DataEndpoint = dataUrl;
            Keyword = keyword;
            CreateDataUrl = createDataUrl;
        }
    }
}