using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankingApp.API.Helpers;
using BankingApp.API.Helpers.BankAccount;
using BankingApp.API.Models.BankAccounts;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.API.Repositories {
    public class BankAccountRepository: IBankAccountRepository {
        private readonly UserManager<User> _userManager;
        private readonly IRepository _repo;

        public BankAccountRepository(IRepository repo, UserManager<User> userManager) {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<BankAccount> CreateAccount(CreateBankAccountModel model, string customerId)
        {
            var user = await _userManager.FindByIdAsync(customerId);
            var customer = await _repo.GetWithWhere<Customer>(o => o.UserId == user.Id);
            var bankAccountType = await _repo.GetById<BankAccountType>(model.AccountType);

            if (customer == null || bankAccountType == null)
                throw new AppException("Unavailable user");

            var bankAccountName = await CreateBankAccount.GenerateBankAccountNumber(bankAccountType.Code, customer);
            var balance = model.InitialDeposit;
            var initialDeposit = model.InitialDeposit;
            var maintenanceFee = bankAccountType.MaintenanceFee;
            var interestRate = bankAccountType.InitialInterestRate ?? 0;

            DateTime? lastDeposit;
            if(initialDeposit > 0) {
                lastDeposit = DateTime.Now;
            } else {
                lastDeposit = null;
            }

            var bankAccount = new BankAccount {
                AccountNumber = bankAccountName,
                Balance = balance,
                MaintenanceFee = maintenanceFee,
                InterestRate = interestRate,
                InitialDeposit = initialDeposit,
                LastDeposit = lastDeposit,
                BankAccountStatus = BankAccountStatus.Active,
                AccountType = bankAccountType,
                Customer = customer
            };

            return bankAccount;
        }
    }
}
