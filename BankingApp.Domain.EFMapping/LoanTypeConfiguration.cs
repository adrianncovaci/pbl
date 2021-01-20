using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class LoanTypeConfiguration: IEntityTypeConfiguration<LoanType> {
        public void Configure(EntityTypeBuilder<LoanType> builder) {
            builder.ToTable("LoanTypes");

            builder.Property(o => o.Type)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasMany(o => o.Loans)
                .WithOne(o => o.LoanType)
                .IsRequired(false);


            builder.HasData(
                new LoanType() {
                    Id = 1,
                    Type = "Mortgage",
                },
                new LoanType() {
                    Id = 2,
                    Type = "Student Loan",
                },
                new LoanType() {
                    Id = 3,
                    Type = "Auto Loan",
                }
            );
        }
    }
}
