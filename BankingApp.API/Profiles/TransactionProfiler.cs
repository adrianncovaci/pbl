using AutoMapper;
using BankingApp.API.Models.Transactions;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionModel>()
                .ForMember(x => x.SenderAccount, y => y.MapFrom(z => z.SenderAccount.AccountNumber))
                .ForMember(x => x.ReceiverCustomerId, y => y.MapFrom(z => z.ReceiverAccount.CustomerId))
                .ForMember(x => x.ReceiverAccountName, y => y.MapFrom(z => z.ReceiverAccount.AccountNumber));
            CreateMap<TransactionModel, Transaction>();
            CreateMap<TransactionModel, DepositWithdrawalModel>();
            CreateMap<DepositWithdrawalModel, TransactionModel>();
        }
    }
}
