using Library.Management.System.Domain.Entities;

namespace Library.Management.System.Application.Repositories;

public interface IBookRepository
{
    public Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task AddAsync(Book book, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Book book, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
