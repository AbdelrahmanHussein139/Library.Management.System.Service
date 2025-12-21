
using Library.Management.System.Application.DTOs.AuthorDTOs;
using FluentValidation;

namespace Library.Management.System.Application.Validators.AuthorDTOsValidators;

public class CreateAuthorDTOValidator : AbstractValidator<CreateAuthorDTO>
{
    public CreateAuthorDTOValidator() : base()
    {

        RuleFor(createAuthorDto => createAuthorDto.Name)
            .NotEmpty().WithMessage("Author name is required.");
        RuleFor(createAuthorDto => createAuthorDto.Bio)
            .NotEmpty().WithMessage("Author bio is required.");

    }
}
