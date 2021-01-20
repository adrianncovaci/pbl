using System;
using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class LoanRequestActionConfiguration: IEntityTypeConfiguration<LoanRequestAction>  {
        public void Configure(EntityTypeBuilder<LoanRequestAction> builder) {
            builder.ToTable("LoanRequestActions");

            builder.Property(o => o.Action)
                .HasConversion(s => s.ToString(),
                               s => (ActionType)Enum.Parse(typeof(ActionType), s))
                .IsRequired();

            builder.Property(o => o.DateAction)
                .IsRequired();

            builder.HasOne(o => o.LoanOfficer)
                .WithMany(o => o.LoanRequestActions);
        }
    }
}
