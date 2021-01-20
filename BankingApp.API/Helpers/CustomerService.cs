// using System;
// using System.Linq;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using System.Threading.Tasks;

// using BankingApp.Domain.EFMapping;
// using BankingApp.API.Helpers;
// using BankingApp.Domain.Entities;
// using BankingApp.API.Models.Customer;

// using Microsoft.EntityFrameworkCore;

// namespace BankingApp.API.Helpers
// {

//     public class CustomerService {
//         private BankContext _context;

//         public CustomerService(BankContext context) {
//             _context = context;
//         }

//         public async Task<Customer> GetById(int id) {
//             var customer = await _context.Customers.FindAsync(id);
//             return customer;
//         }

//         public async Task<IEnumerable<Customer>> GetAll() {
//             var customers = await _context.Customers.ToListAsync();
//             return customers;
//         }

//         public async Task<Customer> Authenticate(string email, string password) {
//             if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
//                 return null;
//             }

//             var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Email == email);

//             if (customer == null)
//                 return null;


//             return customer;
//         }

//         public async Task<RegisterModel> Add(RegisterModel customer) {

//             if (await _context.Customers.AnyAsync(x => x.Email == customer.Email))
//                 throw new AppException("Email is already taken");

//             if (await _context.Customers.AnyAsync(x => x.CNP == customer.CNP))
//                 throw new AppException("User is already registered");

//             return customer;
//         }

//         public async Task<Customer> Update(Customer cust) {
//             var customer = await _context.Customers.FindAsync(cust.Id);

//             if (customer == null)
//                 throw new AppException("User not found!");

//             if (!string.IsNullOrEmpty(cust.Email) && cust.Email != customer.Email ) {

//                 if (await _context.Customers.AnyAsync(x => x.Email == cust.Email))
//                     throw new AppException("Email is already taken");

//                 customer.Email = cust.Email;
//             }

//             if (!string.IsNullOrWhiteSpace(cust.FirstName))
//                 customer.FirstName = cust.FirstName;

//             if (!string.IsNullOrWhiteSpace(cust.LastName))
//                 customer.LastName = cust.LastName;

//             if (!string.IsNullOrWhiteSpace(cust.City))
//                 customer.City = cust.City;

//             if (!string.IsNullOrWhiteSpace(cust.Address))
//                 customer.Address = cust.Address;

//             if (!string.IsNullOrWhiteSpace(cust.ZipCode))
//                 customer.ZipCode = cust.ZipCode;

//             return customer;
//         }
//     }
// }
