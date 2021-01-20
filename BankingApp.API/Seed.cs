using System.Collections.Generic;
using System.Threading.Tasks;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BankingApp.API {
    public class Seed {

        public static async Task SeedUsers(UserManager<User> _adminManager, IRepository _repo) {
            var email = "admin@banca.com";
            if (await _adminManager.FindByEmailAsync(email) == null) {

                var password = "iamr00t";
                var admin = new User {
                    FirstName = "Admin",
                    LastName = "Joestar",
                    Email = email,
                    UserName = email,
                };
                await _adminManager.CreateAsync(admin, password);
                await _adminManager.AddToRoleAsync(admin, "Admin");
                var adminEntity = new Admin() {
                    User = admin
                };


                var customer = new Customer {
                    FirstName = "Jon",
                    LastName = "Snow",
                    CNP = "1233211233212",
                    City = "Los Santos",
                    Address = "Grove St.",
                    ZipCode = "4204"
                };

                var customerUser = new User {
                    FirstName = "Jon",
                    LastName = "Snow",
                    Email = "vanea@zapada.com",
                    UserName = "vanea@zapada.com"
                };

                await _adminManager.CreateAsync(customerUser, "Drag0ns");
                await _adminManager.AddToRoleAsync(customerUser, "Customer");
                customer.User = customerUser;


                var loanOfficer = new LoanOfficer {
                    FirstName = "Private",
                    LastName = "Ryan",
                    City = "'Murica",
                    Address = "Freedom st.",
                    ZipCode = "8008"
                };

                var loanUser = new User {
                    FirstName = "Private",
                    LastName = "Ryan",
                    Email = "defensiva@banca.com",
                    UserName = "defensiva@banca.com"
                };

                await _adminManager.CreateAsync(loanUser, "pr0tection");
                await _adminManager.AddToRoleAsync(loanUser, "LoanOfficer");
                loanOfficer.User = loanUser;

                await _repo.Add(loanOfficer);
                await _repo.Add(adminEntity);
                await _repo.Add(customer);
            }

        }

        public static async Task SeedRoles(RoleManager<Role> roleManager) {
            if ( !await roleManager.RoleExistsAsync("Admin")) {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "Customer" },
                    new Role { Name = "LoanOfficer" }
                };

                foreach (var role in roles) {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
