using System;
using System.Text;
using System.Threading.Tasks;
using BankingApp.API.Models.BankAccounts;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Helpers.BankAccount {
    public class CreateBankAccount {
        public static async Task<string> GenerateBankAccountNumber(string nCode, Customer cust) {
            var branch = "M8D";
            var code = nCode;
            var pers = cust.CNP.Substring(0, 3);
            StringBuilder rng = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 3; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                rng.Append(ch);
            }

            var result = await Task.FromResult<string>($"{branch}{code}{rng}");

            return result;
        }
    }
}
