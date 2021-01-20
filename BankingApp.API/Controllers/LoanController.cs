using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankingApp.API.Helpers;
using BankingApp.API.Infrastructure.Extensions;
using BankingApp.API.Models.BankAccounts;
using BankingApp.API.Models.Loans;
using BankingApp.API.Models.Pagination;
using BankingApp.API.Models.Transactions;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
{
    [AuthorizeAttribute]
    [ApiControllerAttribute]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _customerManager;
        private readonly IBankAccountRepository _bankRepo;
        public LoanController(IRepository repo, IMapper map, UserManager<User> custom, IBankAccountRepository bank)
        {
            _repo = repo;
            _mapper = map;
            _customerManager = custom;
            _bankRepo = bank;
        }

        [HttpPostAttribute()]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanModel model)
        {
            var loan = await _repo.GetByIdWithInclude<Loan>(model.LoanId, o => o.LoanType);
            var customer = await _repo.GetWithWhere<Customer>(o => o.CNP == model.CustomerCNP);
            var create_model = new CreateBankAccountModel
            {
                AccountType = 4,
                InitialDeposit = 10000,
            };
            var bankAccount = await _bankRepo.CreateAccount(create_model, customer.UserId.ToString());
            bankAccount.InterestRate = loan.InterestRate;
            bankAccount.Period = loan.Period;
            var bank = await _repo.Add<BankAccount>(bankAccount);
            return Ok(bank);
        }

        [HttpPostAttribute("request/")]
        public async Task<IActionResult> RequestLoan([FromBody] LoanRequestModel model)
        {
            var userId = Int32.Parse(User.Identity.Name);
            var customer = await _repo.GetWithWhere<Customer>(o => o.UserId == userId);
            model.CustomerId = customer.Id;
            var loanRequest = _mapper.Map<LoanRequest>(model);
            loanRequest.Status = LoanRequestStatus.Pending;
            await _repo.Add(loanRequest);
            return Ok(loanRequest);
        }

        [HttpGetAttribute("requests/")]
        public async Task<IActionResult> GetLoanRequestsByCurrentUser()
        {
            var userId = Int32.Parse(User.Identity.Name);
            var customer = await _repo.GetWithWhere<Customer>(o => o.UserId == userId);
            var loanRequests = await _repo.GetWithWhereList<LoanRequest>(o => o.CustomerId == customer.Id);
            var models = _mapper.Map<IList<LoanRequestModel>>(loanRequests);
            return Ok(models);
        }

        [HttpPostAttribute("allrequests")]
        public async Task<IActionResult> GetAllLoanRequests([FromBody] PagedRequest request)
        {
            var models = await _repo.GetPagedResponse<LoanRequest, LoanRequestModel>(request);
            return Ok(models);
        }

        [AuthorizeAttribute(Policy = "LoanOfficerRole")]
        [HttpPostAttribute("reject/{id}")]
        public async Task<IActionResult> DeclineLoanRequest(int id)
        {
            var loanRequest = await _repo.GetById<LoanRequest>(id);

            if (loanRequest.Status != LoanRequestStatus.Pending)
            {
                throw new AppException("Bank Account already evaluated");
            }

            loanRequest.Status = LoanRequestStatus.Declined;
            var requestAction = await EvaluateLoanRequest(id, ActionType.Reject);
            await _repo.Add(requestAction);
            return Ok();
        }

        [HttpPostAttribute("accept/{id}")]
        public async Task<IActionResult> AcceptLoanRequest(int id, [FromBody] CreateLoanModel model)
        {
            var loanRequest = await _repo.GetById<LoanRequest>(id);

            if (loanRequest.Status != LoanRequestStatus.Pending)
            {
                throw new AppException("Bank Account already evaluated");
            }

            loanRequest.Status = LoanRequestStatus.Accepted;
            var requestAction = await EvaluateLoanRequest(id, ActionType.Accept);
            await _repo.Add(requestAction);
            await CreateLoan(model);
            return Ok(requestAction);
        }

        public async Task<LoanRequestAction> EvaluateLoanRequest(int id, ActionType action)
        {
            var userId = await Task.FromResult<int>(Int32.Parse(User.Identity.Name));
            var officer = await _repo.GetWithWhere<LoanOfficer>(o => o.UserId == userId);
            var model = new EvaluateLoanRequestModel()
            {
                LoanOfficerId = officer.Id,
                LoanRequestId = id,
                Action = action
            };
            var requestAction = _mapper.Map<LoanRequestAction>(model);
            return requestAction;
        }

        [HttpPostAttribute("loans")]
        public async Task<IActionResult> GetLoans([FromBody] PagedRequest model)
        {
            var models = await _repo.GetPagedResponse<Loan, LoanModel>(model);
            var types = await _repo.GetAll<LoanType>();
            foreach (var _model in models.Data)
            {
                foreach (var _type in types)
                {
                    if (_model.LoanTypeId == _type.Id)
                    {
                        _model.Type = _type.Type;
                        break;
                    }
                }
            }
            return Ok(models);
        }

        [HttpPostAttribute("loantypes")]
        public async Task<IActionResult> GetAllLoanTypes([FromBody] PagedRequest model)
        {
            var loans = await _repo.GetWithInclude<Loan>(o => o.LoanType);
            var models = _mapper.Map<IList<LoanModel>>(loans);
            return Ok(models);
        }
    }
}
