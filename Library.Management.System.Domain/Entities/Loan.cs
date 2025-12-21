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

    public void LoanBook(Guid bookId, Guid borrowerId)
    {
        BookId = bookId;
        BorrowerId = borrowerId;
        LoanDate = DateTime.UtcNow;
        ReturnDate = null;
    }

    public void ReturnBook()
    {
        if (!IsActive)
            throw new DomainException("Book already returned");

        ReturnDate = DateTime.UtcNow;
    }

    public bool IsActive => ReturnDate == null;
}
