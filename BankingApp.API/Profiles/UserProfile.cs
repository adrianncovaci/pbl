using AutoMapper;
using BankingApp.API.Models.BankAccounts;
using BankingApp.API.Models.Customer;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Profiles {
    public class UserProfile: Profile {
        public UserProfile() {

            CreateMap<User, RegisterModel>();
            CreateMap<RegisterModel, User>();

            CreateMap<Customer, RegisterModel>();
            CreateMap<RegisterModel, Customer>();
        }
    }
}
