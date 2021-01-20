using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class LoanOfficerConfiguration: IEntityTypeConfiguration<LoanOfficer> {
        public void Configure(EntityTypeBuilder<LoanOfficer> builder) {
            builder.ToTable("LoanOfficers");

            builder.Property(o => o.FirstName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(o => o.LastName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(o => o.City)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(o => o.Address)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(o => o.ZipCode)
                .HasMaxLength(6)
                .IsRequired();
        }
    }
}
