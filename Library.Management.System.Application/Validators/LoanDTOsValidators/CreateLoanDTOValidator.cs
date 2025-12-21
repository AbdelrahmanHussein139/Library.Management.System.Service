
using Library.Management.System.Application.DTOs.LoanDTOs;
using FluentValidation;

namespace Library.Management.System.Application.Validators.LoanDTOsValidators;

public class CreateLoanDTOValidator : AbstractValidator<CreateLoanDTO>
{
    public CreateLoanDTOValidator() : base()
    {

        RuleFor(createLoanDto => createLoanDto.LoanDate)
            .NotEmpty().WithMessage("Author name is required.");
        RuleFor(createLoanDto => createLoanDto.BookId)
            .NotEmpty().WithMessage("Book ID is required.");
        RuleFor(createLoanDto => createLoanDto.BorrowerId)
            .NotEmpty().WithMessage("Borrower ID is required.");



    }
}
