using System.ComponentModel.DataAnnotations;

namespace BankingApp.API.Models.Customer{
    public class RegisterModel {
        [RequiredAttribute]
        public string FirstName { get; set; }
        [RequiredAttribute]
        public string LastName { get; set; }
        [RequiredAttribute]
        public string CNP { get; set; }
        [RequiredAttribute]
        public string Email { get; set; }
        [RequiredAttribute]
        public string Password { get; set; }
        [RequiredAttribute]
        public string City { get; set; }
        [RequiredAttribute]
        public string Address { get; set; }
        [RequiredAttribute]
        public string ZipCode { get; set; }
    }
}
