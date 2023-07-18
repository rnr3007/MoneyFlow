namespace MoneyFlow.Utils
{
    public class ResponseGenerator
    {
        public int StatusCode { get; }

        public string Message { get; }

        public object Data { get; }
        
        public ResponseGenerator(int statusCode, string message, object data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}