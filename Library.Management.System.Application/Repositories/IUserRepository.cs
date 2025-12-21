using Library.Management.System.Domain.Entities;


namespace Library.Management.System.Application.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task AddAsync(User user, CancellationToken cancellationToken = default);
    public Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
