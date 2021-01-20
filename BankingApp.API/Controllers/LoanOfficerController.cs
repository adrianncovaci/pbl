using System.Threading.Tasks;
using AutoMapper;
using BankingApp.API.Helpers;
using BankingApp.API.Models.Customer;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
{
    [AuthorizeAttribute(Policy = "LoanOfficerRole")]
    [ApiControllerAttribute]
    [Route("[controller]")]
    public class LoanOfficerController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository _repo;
        private readonly IMapper _map;
        public LoanOfficerController(UserManager<User> mngr, IRepository repo,
                                     IMapper map)
        {
            _userManager = mngr;
            _repo = repo;
            _map = map;
        }

        [AuthorizeAttribute(Policy = "AdminRole")]
        [HttpPostAttribute("register")]
        public async Task<IActionResult> CreateLoanOfficer([FromBody] RegisterModel model)
        {
            var user = _map.Map<User>(model);
            var officer = _map.Map<LoanOfficer>(model);
            user.UserName = user.Email;

            try
            {
                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "LoanOfficer");
                officer.User = user;
                var result = await _repo.Add(officer);
                return Ok(result);
            }
            catch (AppException e)
            {
                throw new AppException(e.Message);
            }
        }
    }
}
