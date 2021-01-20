using System;
using System.Collections.Generic;

namespace BankingApp.Domain.Entities {
    public class LoanRequestAction: BaseEntity {
        public DateTime DateAction { get; set; } = DateTime.Now;
        public ActionType Action { get; set; }
        public int LoanRequestId { get; set; }
        public LoanRequest LoanRequest { get; set; }
        public int LoanOfficerId { get; set; }
        public LoanOfficer LoanOfficer { get; set; }
    }

    public enum ActionType {
        Accept,
        Reject
    }
}
