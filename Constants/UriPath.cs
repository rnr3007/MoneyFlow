namespace MoneyFlow.Constants
{
    public class UriPath
    {
        public const string EXPENSE = "/expense";
        public const string EXPENSE_CREATE = "create";
        public const string EXPENSE_LIST = "/expenses";
        public const string EXPENSE_UPDATE = "update/{expenseId}";
        public const string EXPENSE_DELETE = "delete/{expenseId}";

        public const string INCOMES = "/incomes";
        public const string INCOMES_CREATE = "/incomes/create";
        public const string INCOMES_UPDATE = "/incomes/update/{incomeId}";
        public const string INCOMES_DELETE = "/incomes/delete/{incomeId}";

        public const string MOTIVATIONS = "/motivations";
        public const string MOTIVATIONS_CREATE = "/motivations/create";
        public const string MOTIVATIONS_UPDATE = "/motivations/update/{motivationId}";
        public const string MOTIVATIONS_DELETE = "/motivations/delete/{motivationId}";

        public const string FILE = "/file";
        public const string GET_FILE = "{fileType}/{purpose}/{fileName}";
        public const string PDF_VIEW = "/file/pdf-view";
        public const string EXCEL_DOWNLOAD = "generate-excel/{purpose}";
        public const string INSERT_FROM_EXCEL = "insert-excel/{purpose}";

        public const string DASHBOARD = "/dashboard";
        public const string ERROR = "/error";
        public const string NOT_FOUND = "/not-found";
    }
}