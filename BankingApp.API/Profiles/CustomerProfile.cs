using System;
using AutoMapper;
using BankingApp.API.Models.BankAccounts;
using BankingApp.API.Models.Customer;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>()
            .ForMember(x => x.Email, y => y.MapFrom(z => z.User.Email))
            .ForMember(x => x.DateJoined, y => y.MapFrom(z => z.User.RegisteredDate));

            CreateMap<CustomerModel, Customer>();

            CreateMap<CreditScoreHistoryModel, CustomerCreditScore>();
            CreateMap<CustomerCreditScore, CreditScoreHistoryModel>();

        }
    }
}

