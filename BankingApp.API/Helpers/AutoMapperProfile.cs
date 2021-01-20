using AutoMapper;
using BankingApp.Domain.Entities;
using BankingApp.API.Models.Customer;

namespace BankingApp.API.Helpers {
    public class AutoMapperProfile: Profile {
        public AutoMapperProfile() {
            CreateMap<Customer, CustomerModel>();
            CreateMap<RegisterModel, Customer>();
            CreateMap<UpdateModel, Customer>();
        }
    }
}
