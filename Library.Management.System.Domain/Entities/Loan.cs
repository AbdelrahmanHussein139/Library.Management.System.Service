using Library.Management.System.Domain.Exceptions;

namespace Library.Management.System.Domain.Entities;

public class Loan:BaseEntity
{
    public DateTime LoanDate { get; private set; }=DateTime.UtcNow;
    public DateTime? ReturnDate { get; private set; }
    public Guid BookId { get; private set; }=Guid.Empty;
    public Book? Book { get; private set; }
    public Guid BorrowerId { get; private set; }=Guid.Empty;
    public Borrower? Borrower { get; private set; }

    
}
