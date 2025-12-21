
using Library.Management.System.Application.DTOs.UserDTOs;
using FluentValidation;

namespace Library.Management.System.Application.Validators.UserDTOsValidators;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator() : base()
    {

        RuleFor(createUserDto => createUserDto.FirstName)
            .NotEmpty().WithMessage("User first name is required.");
        RuleFor(createUserDto => createUserDto.LastName)
            .NotEmpty().WithMessage("User last name is required.");
        RuleFor(createUserDto => createUserDto.Email)
            .NotEmpty().WithMessage("User email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(createUserDto => createUserDto.PasswordHash)
            .NotEmpty().WithMessage("User password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

    }
}
