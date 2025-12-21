using Library.Management.System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Management.System.Infrastructure.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(author => author.Id);

        builder.Property(author => author.Id)
            .ValueGeneratedNever();

        builder.Property(author => author.Name)
            .IsRequired();

        builder.Property(author => author.Bio)
            .IsRequired();

        builder.Property(author => author.CreatedAt)
            .IsRequired();

        builder.Property(author => author.UpdatedAt)
            .IsRequired();

        builder.HasIndex(author => author.Id);
        builder.HasIndex(author => author.Name);
        builder.HasIndex(author => author.CreatedAt);
    }
}
