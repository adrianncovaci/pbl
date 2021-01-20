using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BankingApp.Domain.Entities;
using BankingApp.API.Models.Transactions;

namespace BankingApp.API.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransaction(TransactionModel model);
        Task<TransactionModel> Withdraw(DepositWithdrawalModel model);
        Task<TransactionModel> Deposit(DepositWithdrawalModel model);
        Task<TransactionModel> Transfer(TransactionModel model);
        Task UpdateCreditScore(DepositWithdrawalModel model);
    }
}
