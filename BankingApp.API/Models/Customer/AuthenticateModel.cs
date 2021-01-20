using System.ComponentModel.DataAnnotations;

namespace BankingApp.API.Models.Customer {
    public class AuthenticateModel {
        [RequiredAttribute]
        public string Email { get; set; }
        [RequiredAttribute]
        public string Password { get; set; }
    }
}
