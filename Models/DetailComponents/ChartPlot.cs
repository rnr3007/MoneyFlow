namespace MoneyFlow.Models
{
    public class ChartPlot<T, U>
    {
        public T XAxis { get; set; }
        public U YAxis { get; set; }

        public ChartPlot(T xAxis, U yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }
    }
}