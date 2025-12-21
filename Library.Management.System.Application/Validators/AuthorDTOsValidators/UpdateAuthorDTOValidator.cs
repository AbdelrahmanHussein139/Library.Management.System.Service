
using Library.Management.System.Application.DTOs.AuthorDTOs;
using FluentValidation;

namespace Library.Management.System.Application.Validators.AuthorDTOsValidators;

public class UpdateAuthorDTOValidator : AbstractValidator<UpdateAuthorDTO>
{
    public UpdateAuthorDTOValidator() : base()
    {

        RuleFor(updateAuthorDto => updateAuthorDto.Name)
            .NotEmpty().WithMessage("Author name is required.");
        RuleFor(updateAuthorDto => updateAuthorDto.Bio)
            .NotEmpty().WithMessage("Author bio is required.");

    }
}
