using System.Threading.Tasks;
using BankingApp.API.Models.BankAccounts;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Repositories.Interfaces {
    public interface IBankAccountRepository {
        public Task<BankAccount> CreateAccount(CreateBankAccountModel model, string customerId);
    }
}
