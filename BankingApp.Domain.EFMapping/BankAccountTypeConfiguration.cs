using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.API.Data {
    public class AccountTypeConfiguration: IEntityTypeConfiguration<BankAccountType> {
        public void Configure(EntityTypeBuilder<BankAccountType> builder) {
            builder.ToTable("BankAccountTypes");

            builder.Property(o => o.AccountType)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(o => o.Code)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(o => o.InitialInterestRate)
                .IsRequired(false);

            builder.Property(o => o.MaintenanceFee)
                .IsRequired();

            builder.HasData(
                            new BankAccountType() { Id = 1, AccountType = "Savings", Code = "101", InitialInterestRate = 0.03m, MaintenanceFee = 10m },
                            new BankAccountType() { Id = 2, AccountType = "Checkings", Code = "301", InitialInterestRate = 0m, MaintenanceFee = 10m },
                            new BankAccountType() { Id = 3, AccountType = "Retirement", Code = "901", InitialInterestRate = 0.04m, MaintenanceFee = 0m},
                            new BankAccountType() { Id = 4, AccountType = "Loan", Code = "666", MaintenanceFee = 0m}
                            );
        }
    }
}
