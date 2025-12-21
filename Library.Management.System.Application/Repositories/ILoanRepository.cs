using Library.Management.System.Domain.Entities;

namespace Library.Management.System.Application.Repositories;

public interface ILoanRepository
{
    public Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task AddAsync(Loan loan, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Loan loan, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> IsBookLoanedAsync(Guid bookId, CancellationToken cancellationToken = default);
}
