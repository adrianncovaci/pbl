using System;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Models.Loans {
    public class CreateLoanModel {
        public string CustomerCNP { get; set; }
        public int LoanId { get; set; }
        public DateTime DateAction { get; set; } = DateTime.Now;
    }
}