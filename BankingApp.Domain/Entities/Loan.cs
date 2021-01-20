using System.Collections.Generic;

namespace BankingApp.Domain.Entities {
    public class Loan: BaseEntity {
        public decimal InterestRate { get; set; }
        public int Period { get; set; }
        public bool FixedRate { get; set; }
        public int LoanTypeId { get; set; }
        public LoanType LoanType { get; set; }
        public ICollection<LoanRequest> LoanRequests { get; set; }
    }
}
