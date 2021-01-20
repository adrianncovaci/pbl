using System.Collections.Generic;

namespace BankingApp.Domain.Entities {
    public class LoanType: BaseEntity {
        public string Type { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
