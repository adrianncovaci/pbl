using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankingApp.API.Helpers;
using BankingApp.API.Models.Transactions;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public TransactionRepository(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Transaction> CreateTransaction(TransactionModel model)
        {
            var transaction = _mapper.Map<Transaction>(model);
            await _repo.Add(transaction);
            return transaction;
        }

        public async Task<TransactionModel> Deposit(DepositWithdrawalModel model)
        {
            if (model.Amount <= 0)
            {
                throw new AppException("Invalid amount");
            }

            var account = await _repo.GetById<BankAccount>(model.SenderAccountId);

            if (account == null)
            {
                throw new AppException("Unavailable bank account");
            }

            if (string.Equals(account.BankAccountStatus.ToString(), "frozen", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AppException("Your bank account is frozen!");
            }

            account.Balance += model.Amount;
            var transactionModel = _mapper.Map<TransactionModel>(model);

            if (account.AccountTypeId == 4)
            {
                await UpdateCreditScore(model);
            }

            return transactionModel;
        }

        public async Task<TransactionModel> Withdraw(DepositWithdrawalModel model)
        {
            if (model.Amount <= 0)
            {
                throw new AppException("Invalid amount");
            }

            var account = await _repo.GetById<BankAccount>(model.SenderAccountId);

            if (account == null)
            {
                throw new AppException("Unavailable bank account");
            }

            if (string.Equals(account.BankAccountStatus.ToString(), "frozen", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AppException("Your bank account is frozen!");
            }

            if (account.Balance < model.Amount)
                throw new AppException("Cannot withdraw more than available balance");

            account.Balance -= model.Amount;
            var transactionModel = _mapper.Map<TransactionModel>(model);
            return transactionModel;
        }

        public async Task<TransactionModel> Transfer(TransactionModel model)
        {
            var withdrawModel = _mapper.Map<DepositWithdrawalModel>(model);
            await Withdraw(withdrawModel);
            withdrawModel.SenderAccountId = model.ReceiverAccountId.Value;
            await Deposit(withdrawModel);
            return model;
        }

        public async Task UpdateCreditScore(DepositWithdrawalModel model)
        {
            var account = await _repo.GetByIdWithInclude<BankAccount>(model.SenderAccountId, o => o.Customer);
            var customer = account.Customer;
            var minAmountToPay = (account.Balance / account.Period) * 1 + account.InterestRate;
            if (model.Amount >= minAmountToPay)
            {
                customer.CreditScore += 3;
            }
            else
            {
                customer.CreditScore -= 3;
            }
            await _repo.Add(new CustomerCreditScore
            {
                CreditScore = customer.CreditScore,
                CustomerId = customer.Id
            });
        }
    }
}
