
using Library.Management.System.Application.DTOs.BorrowerDTOs;
using FluentValidation;

namespace Library.Management.System.Application.Validators.BorrowerDTOsValidators;

public class UpdateBorrowerDTOValidator : AbstractValidator<CreateBorrowerDTO>
{
    public UpdateBorrowerDTOValidator() : base()
    {

        RuleFor(createBorrowerDto => createBorrowerDto.Name)
            .NotEmpty().WithMessage("Borrower name is required.");
        RuleFor(createBorrowerDto => createBorrowerDto.Email)
            .NotEmpty().WithMessage("Borrower email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(createBorrowerDto => createBorrowerDto.PhoneNumber)
            .NotEmpty().WithMessage("Borrower phone number is required.")
           .Matches(@"^009627[789]\d{7}$").WithMessage("Invalid Jordanian phone number format.");

    }
}


