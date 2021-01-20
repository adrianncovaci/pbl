using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.Domain.Entities {
    public class User: IdentityUser<int> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; private set; } = DateTime.Now;

        public Admin Admin { get; set; }
        public LoanOfficer LoanOfficer { get; set; }
        public Customer Customer { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
