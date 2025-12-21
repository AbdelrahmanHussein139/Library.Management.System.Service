
using FluentValidation;
using Library.Management.System.Application.DTOs.UserDTOs;

namespace Library.Management.System.Application.Validators.UserDTOsValidators;

public class SignInUserDTOValidator: AbstractValidator<SignInUserDTO>
{
   public SignInUserDTOValidator(): base()
   {
        RuleFor(SignInUserDto => SignInUserDto.Email)
            .NotEmpty().WithMessage("User email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(SignInUserDto => SignInUserDto.PasswordHash)
                .NotEmpty().WithMessage("User password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
