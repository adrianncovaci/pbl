using System;
using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class LoanRequestConfiguration: IEntityTypeConfiguration<LoanRequest> {
        public void Configure(EntityTypeBuilder<LoanRequest> builder) {

            builder.Property(o => o.DateIssued)
                .IsRequired();

            builder.Property(o => o.Comments)
                .IsRequired();

            builder.Property(o => o.ResponseDate)
                .ValueGeneratedOnUpdate();

            builder.Property(o => o.Status)
                .HasConversion(s => s.ToString(),
                               s => (LoanRequestStatus)Enum.Parse(typeof(LoanRequestStatus), s))
                .IsRequired();

            builder.HasOne(o => o.Loan)
                .WithMany(o => o.LoanRequests)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Customer)
                .WithMany(o => o.LoanRequests)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.LoanRequestAction)
                .WithOne(o => o.LoanRequest)
                .HasForeignKey<LoanRequestAction>(o => o.LoanRequestId);
        }
    }
}
