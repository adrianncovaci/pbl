using System;

namespace BankingApp.Domain.Entities {
    public class LoanRequest: BaseEntity {
        public DateTime DateIssued { get; private set; } = DateTime.Now;
        public string Comments { get; set; }
        public DateTime? ResponseDate { get; set; }

        public LoanRequestStatus Status { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        public LoanRequestAction LoanRequestAction { get; set; }
    }

    public enum LoanRequestStatus {
        Pending,
        Accepted,
        Declined
    }
}
