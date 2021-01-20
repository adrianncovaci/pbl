using System;

namespace BankingApp.Domain.Entities
{
    public class CustomerCreditScore : BaseEntity
    {
        public decimal CreditScore { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}