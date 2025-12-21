using Library.Management.System.Application.DTOs.LoanDTOs;


namespace Library.Management.System.Application.Services.LoanServices;

public interface ILoanService
{
    Task<LoanDTO?> GetByIdAsync(Guid id, string? userId, string? userRole, CancellationToken cancellationToken = default);
    Task<List<LoanDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LoanDTO> CreateAsync(CreateLoanDTO createDto, CancellationToken cancellationToken = default);
    Task<LoanDTO?> UpdateAsync(Guid id, UpdateLoanDTO updateDto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsBookLoanedAsync(Guid bookId, CancellationToken cancellationToken = default);
}
