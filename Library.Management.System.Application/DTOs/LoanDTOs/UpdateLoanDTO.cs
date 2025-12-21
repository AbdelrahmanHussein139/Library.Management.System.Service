

namespace Library.Management.System.Application.DTOs.LoanDTOs;

public class UpdateLoanDTO
{
    public DateTime LoanDate { get;  set; } = DateTime.UtcNow;
    public DateTime? ReturnDate { get; set; }
    public Guid BookId { get;  set; } = Guid.Empty;
    public Guid BorrowerId { get;  set; } = Guid.Empty;
}
