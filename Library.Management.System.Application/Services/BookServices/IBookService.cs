using Library.Management.System.Application.DTOs.BookDTOs;


namespace Library.Management.System.Application.Services.BookServices;

public interface IBookService
{
    public Task<BookDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<BookDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<BookDTO> CreateAsync(CreateBookDTO createDto, CancellationToken cancellationToken = default);
    public Task<BookDTO?> UpdateAsync(Guid id, UpdateBookDTO updateDto, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
