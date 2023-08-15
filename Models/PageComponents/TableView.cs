using System.Collections.Generic;
using MoneyFlow.Models.DetailComponents;

namespace MoneyFlow.Models
{
    public class TableView<T>
    {
        public List<T> Data { get; set; }

        public Pagination PaginationView { get; set; }

        public SearchBar SearchBarView { get; set; }

        public TableView(List<T> data, Pagination paginationView)
        {
            Data = data;
            PaginationView = paginationView;
        }
    }
}