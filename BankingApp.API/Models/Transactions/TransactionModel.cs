using System;

namespace BankingApp.API.Models.Transactions {
    public class TransactionModel {
        public int SenderAccountId { get; set; }
        public string SenderAccount { get; set; }
        public string ReceiverAccountName { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public int? ReceiverAccountId { get; set; }
        public int ReceiverCustomerId { get; set; }
        public string Message { get; set; }
        public string TransactionType { get; set; }
    }
}
