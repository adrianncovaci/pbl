using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class UserConfiguration: IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable("Users");
            builder.Property(o => o.FirstName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(o => o.LastName)
                .HasMaxLength(32)
                .IsRequired();

            // One to one relationship with the Admin Entity
            builder.HasOne(o => o.Admin)
                .WithOne(o => o.User)
                .HasForeignKey<Admin>(o => o.UserId);

            // One to one relationship with the LoanOfficer Entity
            builder.HasOne(o => o.LoanOfficer)
                .WithOne(o => o.User)
                .HasForeignKey<LoanOfficer>(o => o.UserId);

            // One to one relationship with the Customer Entity
            builder.HasOne(o => o.Customer)
                .WithOne(o => o.User)
                .HasForeignKey<Customer>(o => o.UserId);
        }
    }
}
