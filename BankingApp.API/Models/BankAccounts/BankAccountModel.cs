using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.API.Models.BankAccounts {
    public class BankAccountModel {
        public int Id { get; set;}
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal MaintenanceFee { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InitialDeposit { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastDeposit { get;  set; }
        public int? Period { get; set; }
        public string BankAccountStatus { get; set; }
        public string AccountType { get; set; }
    }
}
