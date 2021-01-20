using System;

namespace BankingApp.API.Models.Transactions {
    public class DepositWithdrawalModel {
        public int SenderAccountId { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
    }
}
