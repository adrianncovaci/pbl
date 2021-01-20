using System;
using System.Threading.Tasks;
using BankingApp.API.Repositories.Interfaces;
using BankingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankingApp.API.Infrastructure.Configurations {
    public static class HostExtensions {
        public static async Task SeedData(this IHost host) {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;

                try {
                    var context = services.GetRequiredService<BankContext>();
                    var adminManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    var repo = services.GetRequiredService<IRepository>();
                    context.Database.Migrate();

                    await Seed.SeedRoles(roleManager);
                    await Seed.SeedUsers(adminManager, repo);
                }
                catch(Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error during migration");
                }
            }
        }
    }
}
