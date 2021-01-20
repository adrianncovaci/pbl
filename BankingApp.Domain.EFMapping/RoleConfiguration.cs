using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class RoleConfiguration: IEntityTypeConfiguration<Role> {
        public void Configure(EntityTypeBuilder<Role> builder) {
        }
    }
}
