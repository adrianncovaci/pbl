using System.ComponentModel.DataAnnotations;

namespace BankingApp.API.Models.BankAccounts {
    public class CreateBankAccountModel {
        [RequiredAttribute]
        public int AccountType { get; set; }
        [RequiredAttribute]
        // [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal InitialDeposit { get; set; }
    }
}
