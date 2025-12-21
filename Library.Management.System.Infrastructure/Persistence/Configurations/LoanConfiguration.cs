using Library.Management.System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Management.System.Infrastructure.Persistence.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("Loans");

        builder.HasKey(loan => loan.Id);

        builder.Property(loan => loan.Id)
            .ValueGeneratedNever();

        builder.Property(loan => loan.BookId)
             .IsRequired();

        builder.Property(loan => loan.BorrowerId)
            .IsRequired();

        builder.Property(loan => loan.LoanDate)
            .IsRequired();

        builder.HasOne(loan => loan.Book)
               .WithMany()
               .HasForeignKey(loan =>loan.BookId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(loan => loan.Borrower)
              .WithMany()
              .HasForeignKey(loan => loan.BorrowerId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.Property(loan => loan.CreatedAt)
            .IsRequired();

        builder.Property(loan => loan.UpdatedAt)
            .IsRequired();

        builder.HasIndex(loan => loan.Id);
        builder.HasIndex(loan => loan.BookId);
        builder.HasIndex(loan => loan.BorrowerId);
        builder.HasIndex(loan => loan.CreatedAt);

    }
}
