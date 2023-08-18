namespace MoneyFlow.Constants
{
    public class RoleConstants
    {
        public const string USER = "User";
        public const string ADMIN = "Admin"        ;
    }

    public class MiscConstants
    {
        public const string X_HEADER_USER_ID = "userId";
        public const string AUTH_CLAIM = "Authentication";
        public const string USER_ID_CLAIM = "UserId";
        public const string TOKEN_BEARER_CLAIM = "TokenBearer";
    }

    public class GeneralConstants
    {
        public const string PURPOSE_EXPENSE = "expense";
        public const string PURPOSE_INCOMES = "incomes";
        public const string PURPOSE_MOTIVATIONS = "motivations";
    }

    public class OrderConstants
    {
        public const string ASC = "Asc";
        public const string DESC = "Desc";
        public const string ORDER_BY_DATE = "OrderDate";
        public const string ORDER_BY_NAME = "OrderName";
        public const string ORDER_BY_MONEY = "OrderMoney";
    }
}