
using System;
using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class PaginationViewModel
    {
        public int ChoosenPage { get; set; } = 1;

        public int EndPage { get; set; } = 1;

        public int LimitData { get; set; } = 10;

        public int TotalData { get; set; } = 1;
        
        public string SearchKeyword { get; set; } = "";

        public string ActionUrl { get; set; } = "";

        public List<string> PageList { get; set; } = new List<string>{"1"};

        public PaginationViewModel(int choosenPage, int limitData, int totalData, string keyword, string actionUrl)
        {
            SearchKeyword = keyword;
            LimitData = limitData < 0 ? 10 : limitData;                                                                                                                   
            TotalData = totalData;
            ActionUrl = actionUrl;
            EndPage = (int)Math.Ceiling((double)totalData / limitData );
            ChoosenPage = choosenPage < 1 || choosenPage > EndPage ? 1 : choosenPage;

            int pageRange = 2;
            int leftPageRange = choosenPage - pageRange;
            int rightPageRange = choosenPage + pageRange;
            
            List<int> pageList = new List<int>();
            List<string> formattedPageList = new List<string>();

            for (int i = 1; i <= EndPage; i++)
            {
                if (i == 1 || 1 == EndPage || (i >= leftPageRange && i <= rightPageRange))
                {
                    pageList.Add(i);
                }
            }

            int pageToBeInserted = 0;
            foreach (int page in pageList)
            {
                if (pageToBeInserted > 0)
                {
                    int pageDelta = page - pageToBeInserted;
                    if (pageDelta == 2)
                    {
                        formattedPageList.Add(pageToBeInserted.ToString());
                    } else if (pageDelta != 1)
                    {
                        formattedPageList.Add("...");
                    }
                }
                pageToBeInserted = page;
                formattedPageList.Add(pageToBeInserted.ToString());
            }
            PageList = formattedPageList;
        }
    }
}