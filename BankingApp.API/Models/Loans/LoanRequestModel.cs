using System;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Models.Loans {
    public class LoanRequestModel {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public string LoanName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public string Status { get; set; } = Enum.GetName(typeof(LoanRequestStatus), LoanRequestStatus.Pending);
        public string Comments { get; set; }
    }
}
