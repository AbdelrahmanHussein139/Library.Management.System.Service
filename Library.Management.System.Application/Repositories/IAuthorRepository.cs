using Library.Management.System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Management.System.Application.Repositories;

public interface IAuthorRepository
{
    public Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Author>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task AddAsync(Author author, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Author author, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
