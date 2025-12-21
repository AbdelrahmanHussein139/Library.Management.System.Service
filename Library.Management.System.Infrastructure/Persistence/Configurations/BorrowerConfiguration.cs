using Library.Management.System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Management.System.Infrastructure.Persistence.Configurations;

public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
{
    public void Configure(EntityTypeBuilder<Borrower> builder)
    {
        builder.ToTable("Borrowers");

        builder.HasKey(borrower => borrower.Id);

        builder.Property(borrower => borrower.Id)
            .ValueGeneratedNever();

        builder.Property(borrower => borrower.Name)
            .IsRequired();

        builder.Property(borrower => borrower.Email)
            .IsRequired();

        builder.Property(borrower => borrower.PhoneNumber)
            .IsRequired();

        builder.Property(borrower => borrower.CreatedAt)
            .IsRequired();

        builder.Property(borrower => borrower.UpdatedAt)
            .IsRequired();

        builder.HasIndex(borrower => borrower.Id);
        builder.HasIndex(borrower => borrower.Name);
        builder.HasIndex(borrower => borrower.CreatedAt);
    }
}
