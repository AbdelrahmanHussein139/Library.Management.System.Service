using Library.Management.System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Management.System.Application.Repositories;

public interface IBorrowerRepository
{
    public Task<Borrower?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Borrower>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task AddAsync(Borrower borrower, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Borrower borrower, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
