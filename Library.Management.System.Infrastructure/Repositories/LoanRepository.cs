using Library.Management.System.Application.Repositories;
using Library.Management.System.Domain.Entities;
using Library.Management.System.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Management.System.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;

    public LoanRepository(LibraryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Loans
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Loans
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
    {
        await _context.Loans.AddAsync(loan, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Loan loan, CancellationToken cancellationToken = default)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var loan = await _context.Loans.FindAsync(new object[] { id }, cancellationToken);
        if (loan != null)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Loans
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, cancellationToken);
    }
    public async Task<bool> IsBookLoanedAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        return await _context.Loans
            .AsNoTracking()
            .AnyAsync(loan => loan.BookId == bookId && loan.ReturnDate == null, cancellationToken);

    }
}
