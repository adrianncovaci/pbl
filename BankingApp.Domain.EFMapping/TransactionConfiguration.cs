using System;
using BankingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingApp.Domain.EFMapping {
    public class TransactionConfigure: IEntityTypeConfiguration<Transaction> {
        public void Configure(EntityTypeBuilder<Transaction> builder) {
            builder.HasOne(o => o.SenderAccount)
                .WithMany(o => o.SenderTransactions)
                .HasForeignKey(o => o.SenderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ReceiverAccount)
                .WithMany(o => o.ReceiverTransactions)
                .HasForeignKey(o => o.ReceiverAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.Property(o => o.DateIssued)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder.Property(o => o.TransactionType)
                .IsRequired();
        }
    }
}
