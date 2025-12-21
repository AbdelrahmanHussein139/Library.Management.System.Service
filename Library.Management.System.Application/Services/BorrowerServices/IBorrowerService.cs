

using Library.Management.System.Application.DTOs.BorrowerDTOs;

namespace Library.Management.System.Application.Services.BorrowerServices;

public interface IBorrowerService
{
    public Task<BorrowerDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<BorrowerDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<BorrowerDTO> CreateAsync(CreateBorrowerDTO createDto, CancellationToken cancellationToken = default);
    public Task<BorrowerDTO?> UpdateAsync(Guid id, UpdateBorrowerDTO updateDto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
