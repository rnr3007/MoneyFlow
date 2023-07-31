namespace MoneyFlow.Models.ViewModels
{
    public class PlotDataModel<T, U>
    {
        public T XAxis { get; set; }
        public U YAxis { get; set; }

        public PlotDataModel(T xAxis, U yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }
    }
}