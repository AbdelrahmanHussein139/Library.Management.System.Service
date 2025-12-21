using Library.Management.System.Application.Repositories;
using Library.Management.System.Domain.Entities;
using Library.Management.System.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Management.System.Infrastructure.Repositories;

public class BorrowerRepository : IBorrowerRepository
{
    private readonly LibraryDbContext _context;

    public BorrowerRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Borrower?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Borrowers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<List<Borrower>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Borrowers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Borrower borrower, CancellationToken cancellationToken = default)
    {
        await _context.Borrowers.AddAsync(borrower, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Borrower borrower, CancellationToken cancellationToken = default)
    {
        _context.Borrowers.Update(borrower);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var borrower = await _context.Borrowers.FindAsync(new object[] { id }, cancellationToken);
        if (borrower != null)
        {
            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Borrowers
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, cancellationToken);
    }
}
