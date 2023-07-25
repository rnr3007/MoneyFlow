using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class TableViewModel<T>
    {
        private List<DataViewModel<Product, byte[]>> list;

        public List<T> Data { get; set; }

        public PaginationViewModel PaginationView { get; set; }

        public TableViewModel(List<T> data, PaginationViewModel paginationView)
        {
            Data = data;
            PaginationView = paginationView;
        }
    }
}