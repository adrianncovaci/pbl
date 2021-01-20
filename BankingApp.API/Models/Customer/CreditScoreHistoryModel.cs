using System;

namespace BankingApp.API.Models.Customer
{
    public class CreditScoreHistoryModel
    {
        public DateTime DateIssued { get; set; }
        public decimal CreditScore { get; set; }
    }
}