using System;
using AutoMapper;
using BankingApp.API.Models.BankAccounts;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Profiles {
    public class BankAccountProfile: Profile {
        public BankAccountProfile() {
            CreateMap<BankAccount, BankAccountModel>()
            .ForMember(x => x.BankAccountStatus, y => y.MapFrom(val => Enum.GetName(typeof(BankAccountStatus), val.BankAccountStatus)));
            CreateMap<BankAccount, BankAccountModel>()
                .ForMember(x => x.AccountType, y => y.MapFrom(z => z.AccountType.AccountType));
            CreateMap<BankAccountModel, BankAccount>();
        }
    }
}
