using System;
using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer> {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.ToTable("Customers");

            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Accounts)
                .WithOne(o => o.Customer);

            builder.HasIndex(o => o.CNP).IsUnique();

            builder.Property(o => o.CNP)
                .HasMaxLength(13)
                .IsRequired();

            builder.Property(o => o.FirstName).HasMaxLength(32).IsRequired();
            builder.Property(o => o.LastName).HasMaxLength(32).IsRequired();

            builder.Property(o => o.CreditScore).HasDefaultValue(540)
                .IsRequired();

            builder.Property(o => o.City)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(o => o.Address)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(o => o.ZipCode).HasMaxLength(6).IsRequired();
        }
    }
}
