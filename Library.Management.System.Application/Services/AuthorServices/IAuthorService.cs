using Library.Management.System.Application.DTOs.AuthorDTOs;    
namespace Library.Management.System.Application.Services.AuthorServices;

public interface IAuthorService
{
    public Task<AuthorDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<AuthorDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<AuthorDTO> CreateAsync(CreateAuthorDTO createDto, CancellationToken cancellationToken = default);
    public Task<AuthorDTO?> UpdateAsync(Guid id, UpdateAuthorDTO updateDto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
