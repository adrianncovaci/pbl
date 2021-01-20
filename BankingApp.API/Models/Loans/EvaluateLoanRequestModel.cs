using System;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Models.Loans {
    public class EvaluateLoanRequestModel {
        public int LoanOfficerId { get; set; }
        public int LoanRequestId { get; set; }
        public ActionType Action { get; set; }
        public DateTime DateAction { get; set; } = DateTime.Now;
    }
}
