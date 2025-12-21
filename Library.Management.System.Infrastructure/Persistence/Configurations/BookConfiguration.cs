using Library.Management.System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Management.System.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(book => book.Id);

        builder.Property(book => book.Id)
            .ValueGeneratedNever();

        builder.Property(book => book.AuthorId)
             .IsRequired();

        builder.Property(book => book.Title)
            .IsRequired();

        builder.HasOne(book => book.Author)
               .WithMany()
               .HasForeignKey(book => book.AuthorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(book => book.PublishedDate)
            .IsRequired();

        builder.Property(book => book.ISBN)
            .IsRequired();
        builder.Property(book => book.CreatedAt)
            .IsRequired();

        builder.Property(book => book.UpdatedAt)
            .IsRequired();

        builder.HasIndex(book => book.Id);
        builder.HasIndex(book => book.Title);
        builder.HasIndex(book => book.CreatedAt);
       
    }
}
