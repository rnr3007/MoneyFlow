
using System;
using System.Collections.Generic;

namespace MoneyFlow.Models
{
    public class Pagination
    {
        public int ChoosenPage { get; set; } = 1;

        public int EndPage { get; set; } = 1;

        public int LimitData { get; set; } = 10;

        public int TotalData { get; set; } = 1;
        
        public string SearchKeyword { get; set; } = "";

        public string ActionUrl { get; set; } = "";

        public List<string> PageList { get; set; } = new List<string>{"1"};

        public  string Order { get; set; } = "";

        public Pagination(int choosenPage, int limitData, int totalData, string keyword, string actionUrl)
        {
            SearchKeyword = keyword;
            LimitData = limitData < 0 ? 10 : limitData;                                                                                                                   
            TotalData = totalData;
            ActionUrl = actionUrl;
            EndPage = (int)Math.Ceiling((double)totalData / limitData );
            ChoosenPage = choosenPage < 1 || choosenPage > EndPage ? 1 : choosenPage;

            int pageRange = 1;
            int leftPageRange = choosenPage - pageRange;
            int rightPageRange = choosenPage + pageRange;
            
            List<int> pageList = new List<int>();
            List<string> formattedPageList = new List<string>();
            for (int i = 1; i <= EndPage; i++)
            {
                if (i == 1 || i == EndPage || (i >= leftPageRange && i <= rightPageRange))
                {
                    pageList.Add(i);
                }
            }

            int pageTotal = pageList.Count;
            if (pageTotal == 1)
            {
                formattedPageList.Add("1");
            } else if (pageTotal > 1)
            {
                for (int i = 0; i < pageTotal - 1; i++)
                {
                    int pageToBeInserted = pageList[i];
                    int nextPage = pageList[i + 1];
                    int pageDelta = nextPage - pageToBeInserted;
                    
                    if (pageDelta == 1)
                    {
                        formattedPageList.Add(pageToBeInserted.ToString());
                    } else
                    {
                        if (i == 0)
                        {
                            formattedPageList.Add(pageToBeInserted.ToString());
                        } else if (i == pageTotal - 2)
                        {
                            formattedPageList.Add(pageList[i].ToString());
                        }
                        formattedPageList.Add("...");
                    }                    

                    if (i == pageTotal - 2)
                    {
                        formattedPageList.Add(pageList[i + 1].ToString());
                    }
                }
            }
            PageList = formattedPageList;
        }
    }
}