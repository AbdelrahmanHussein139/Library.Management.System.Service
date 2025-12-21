using Microsoft.EntityFrameworkCore;
using Library.Management.System.Domain.Entities;
using Library.Management.System.Infrastructure.Persistence.Configurations;

namespace Library.Management.System.Infrastructure.Persistence.DbContext;

public class LibraryDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }  
    public DbSet<Borrower> Borrowers { get; set; }  
    public DbSet<Loan> Loans { get; set; }  
    public DbSet<User> Users { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new BorrowerConfiguration());
        modelBuilder.ApplyConfiguration(new LoanConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
