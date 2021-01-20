using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

using BankingApp.API.Helpers;
using BankingApp.API.Models.Customer;
using BankingApp.Domain.Entities;
using BankingApp.Domain.EFMapping;
using Microsoft.AspNetCore.Identity;
using BankingApp.API.Repositories.Interfaces;

namespace BankingApp.API.Controllers
{
    [AuthorizeAttribute]
    [ApiControllerAttribute]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppSettings _settings;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        private readonly SignInManager<User> _signInManager;

        public CustomerController(IRepository repo, IMapper mapper,
                                  IOptions<AppSettings> appSettings, UserManager<User> manager,
                                  SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _settings = appSettings.Value;
            _repo = repo;
            _userManager = manager;
            _signInManager = signInManager;
        }

        [AllowAnonymousAttribute]
        [HttpPostAttribute("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return BadRequest(new { message = "Incorrect email and/or password" });
            var roles = await _userManager.GetRolesAsync(user);
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName.ToString()),
                        new Claim("RegisteredDate", user.RegisteredDate.ToString()),
                        new Claim("Id", user.Id.ToString()),
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
            };
            foreach (var role in roles)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                tokenDescriptor.Subject.AddClaim(claim);
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
            var claims = claimsPrincipal.Claims.ToList();

            return Ok(new
            {
                Token = tokenString,
            });
        }

        [AllowAnonymousAttribute]
        [HttpPostAttribute("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = _mapper.Map<User>(model);
            var customer = _mapper.Map<Customer>(model);

            user.UserName = user.Email;
            try
            {
                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "Customer");
                customer.User = user;
                var result = await _repo.Add(customer);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPostAttribute("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _signInManager.RefreshSignInAsync(user);
            return Ok();
        }

        [HttpGetAttribute]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _repo.GetAll<Customer>();
            var models = _mapper.Map<IList<CustomerModel>>(customers);

            return Ok(customers);
        }

        [HttpGetAttribute("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _repo.GetByIdWithInclude<Customer>(id, o => o.User);
            var model = _mapper.Map<CustomerModel>(customer);
            model.Email = customer.User.Email;
            model.DateJoined = customer.User.RegisteredDate;
            return Ok(model);
        }

        [HttpGetAttribute("loggedin")]
        public async Task<IActionResult> GetLoggedInCustomer()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);
            var customer = await _repo.GetWithWhere<Customer>(o => o.UserId == user.Id);
            customer.User = user;
            var model = _mapper.Map<CustomerModel>(customer);
            model.Email = customer.User.Email;
            model.DateJoined = customer.User.RegisteredDate;
            return Ok(model);
        }

        [HttpPutAttribute("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateModel model)
        {
            var cust = _mapper.Map<Customer>(model);
            var customer = await _repo.GetById<Customer>(id);

            if (customer == null)
                throw new AppException("User not found!");

            if (!string.IsNullOrEmpty(cust.User.Email) && cust.User.Email != customer.User.Email)
            {

                if (await _userManager.FindByEmailAsync(cust.User.Email) != null)
                    throw new AppException("Email is already taken");

                customer.User.Email = cust.User.Email;
            }

            if (!string.IsNullOrWhiteSpace(cust.FirstName))
                customer.FirstName = cust.FirstName;

            if (!string.IsNullOrWhiteSpace(cust.LastName))
                customer.LastName = cust.LastName;

            if (!string.IsNullOrWhiteSpace(cust.City))
                customer.City = cust.City;

            if (!string.IsNullOrWhiteSpace(cust.Address))
                customer.Address = cust.Address;

            if (!string.IsNullOrWhiteSpace(cust.ZipCode))
                customer.ZipCode = cust.ZipCode;

            await _userManager.UpdateAsync(customer.User);
            return Ok();
        }


        [HttpDeleteAttribute("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cust = await _repo.GetById<Customer>(id);
            await _userManager.DeleteAsync(cust.User);
            await _repo.Delete<Customer>(id);
            return Ok();
        }

        [HttpGetAttribute("scorehistory/{id}")]
        public async Task<IActionResult> GetCustomerCreditScoreHistory(int id)
        {
            var data = await _repo.GetWithWhereList<CustomerCreditScore>(o => o.CustomerId == id);
            var models = _mapper.Map<IList<CreditScoreHistoryModel>>(data);
            return Ok(models);
        }

    }
}
