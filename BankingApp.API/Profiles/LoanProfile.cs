using AutoMapper;
using BankingApp.API.Models.Customer;
using BankingApp.API.Models.Loans;
using BankingApp.Domain.Entities;

namespace BankingApp.API.Profiles
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {

            CreateMap<LoanRequest, LoanRequestModel>()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Customer.CNP))
                .ForMember(x => x.CustomerId, y => y.MapFrom(z => z.Customer.Id))
                .ForMember(x => x.LoanName, y => y.MapFrom(z => z.Loan.LoanType.Type))
                .ForMember(x => x.Status, y => y.MapFrom(z => z.Status));

            CreateMap<Loan, LoanModel>()
                .ForMember(x => x.Type, y => y.MapFrom(z => z.LoanType.Type));

            CreateMap<LoanModel, Loan>();
            CreateMap<LoanRequestModel, LoanRequest>();
            CreateMap<LoanRequestAction, EvaluateLoanRequestModel>();
            CreateMap<EvaluateLoanRequestModel, LoanRequestAction>();

            CreateMap<LoanOfficer, RegisterModel>();
            CreateMap<RegisterModel, LoanOfficer>();
        }
    }
}
