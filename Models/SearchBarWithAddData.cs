using System.Collections.Generic;

namespace MoneyFlow.Models
{
    public class SearchBarWithAddData
    {
        public string DataEndpoint { get; set; }

        public string Keyword { get; set; }
        
        public string CreateDataUrl { get; set; }

        public bool IsFilterable { get; set; } = false;

        public List<string> FilterList { get; set; }

        public SearchBarWithAddData(string dataUrl, string keyword, string createDataUrl)
        {
            DataEndpoint = dataUrl;
            Keyword = keyword;
            CreateDataUrl = createDataUrl;
        }

        public SearchBarWithAddData(string dataUrl, string keyword, string createDataUrl, List<string> filterList)
        {
            DataEndpoint = dataUrl;
            Keyword = keyword;
            CreateDataUrl = createDataUrl;
            IsFilterable = true;
            FilterList = filterList;
        }
    }
}