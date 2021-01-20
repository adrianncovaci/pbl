using System;

namespace BankingApp.API.Models.Customer
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }
        public decimal CreditScore { get; set; }
        public string CNP { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
