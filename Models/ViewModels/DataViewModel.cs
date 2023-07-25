namespace MoneyFlow.Models.ViewModels
{
    public class DataViewModel<T, U>
    {
        public T Data { get; set; }

        public U SpecialData { get; set; }

        public DataViewModel(T data, U specialData)
        {
            Data = data;
            SpecialData = specialData;
        }
    }
}