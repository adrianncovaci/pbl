using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class LoanConfiguration: IEntityTypeConfiguration<Loan> {
        public void Configure(EntityTypeBuilder<Loan> builder) {
            builder.ToTable("Loans");

            builder.Property(o => o.InterestRate)
                .IsRequired();
            builder.Property(o => o.Period)
                .IsRequired();
            builder.Property(o => o.FixedRate)
                .IsRequired();

            builder.HasOne(o => o.LoanType)
                .WithMany(o => o.Loans)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Loan {
                    Id = 1,
                    InterestRate = 0.03m,
                    Period = 180,
                    FixedRate = true,
                    LoanTypeId = 1,
                },
                new Loan {
                    Id = 2,
                    InterestRate = 0.0355m,
                    Period = 360,
                    FixedRate = true,
                    LoanTypeId = 1,
                },
                new Loan {
                    Id = 3,
                    InterestRate = 0.0435m,
                    Period = 48,
                    FixedRate = false,
                    LoanTypeId = 3,
                },
                new Loan {
                    Id = 4,
                    InterestRate = 0.0437m,
                    Period = 60,
                    FixedRate = false, 
                    LoanTypeId = 3,
                }
            );
        }
    }
}
