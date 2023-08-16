using System;

namespace MoneyFlow.Data.Views
{
    public class UserAndMoney
    {
        public string UserId { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public long TotalIncome { get; set; }

        public long TotalExpense { get; set; }
    }
}