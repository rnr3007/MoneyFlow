namespace MoneyFlow.Constants
{
    public class UriPath
    {
        public const string EXPENSE = "/expense";
        public const string EXPENSE_CREATE = "create";
        public const string EXPENSE_LIST = "/expenses";
        public const string EXPENSE_UPDATE = "update/{expenseId}";
        public const string EXPENSE_DELETE = "delete/{expenseId}";

        public const string FILE = "/file";
        public const string GET_FILE = "{fileType}/{purpose}/{fileName}";
        public const string PDF_VIEW = "/file/pdf-view";

        public const string ERROR = "/error";
    }
}