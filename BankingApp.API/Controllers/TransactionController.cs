using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankingApp.API.Models.Pagination;
using BankingApp.API.Models.Transactions;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Controllers
{
    [AuthorizeAttribute]
    [ApiControllerAttribute]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public TransactionController(IRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        [HttpPostAttribute("transactions/{id}")]
        public async Task<IActionResult> GetTransactionsByUser(int id, [FromBody] PagedRequest pagedRequest)
        {
            var bankAccount = await _repo.GetById<BankAccount>(id);

            pagedRequest.RequestFilters.Add(new RequestFilters
            {
                FilterOperators = FilterOperators.And,
                Filters = new List<Filter> { new Filter { Path = "senderaccount.accountnumber", Value = bankAccount.AccountNumber } }
            });

            var models = await _repo.GetPagedResponse<Transaction, TransactionModel>(pagedRequest);
            foreach (var filter in pagedRequest.RequestFilters)
            {
                if (filter.StartDate != null)
                {
                    models.Data = models.Data.Where(x => x.DateIssued >= filter.StartDate).ToList();
                }
                if (filter.EndDate != null)
                {
                    models.Data = models.Data.Where(x => x.DateIssued <= filter.EndDate).ToList();
                }
            }
            return Ok(models);
        }
    }
}