using Library.Management.System.Application.Repositories;
using Library.Management.System.Domain.Entities;
using Library.Management.System.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Management.System.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    public AuthorRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<List<Author>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
    {
        await _context.Authors.AddAsync(author, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Author author, CancellationToken cancellationToken = default)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _context.Authors.FindAsync(new object[] { id }, cancellationToken);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, cancellationToken);
    }


}
