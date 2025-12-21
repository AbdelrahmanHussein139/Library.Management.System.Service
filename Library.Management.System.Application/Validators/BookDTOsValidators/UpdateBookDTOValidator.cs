
using Library.Management.System.Application.DTOs.BookDTOs;
using FluentValidation;

namespace Library.Management.System.Application.Validators.BookDTOsValidators;

public class UpdateBookDTOValidator : AbstractValidator<UpdateBookDTO>
{
    public UpdateBookDTOValidator() : base()
    {

        RuleFor(createBookDto => createBookDto.Title)
            .NotEmpty().WithMessage("Book title is required.");
        RuleFor(createBookDto => createBookDto.AuthorId)
            .NotEmpty().WithMessage("Author ID is required.");
        RuleFor(createBookDto => createBookDto.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Length(13).WithMessage("ISBN must be 13 characters long.");
        RuleFor(createBookDto => createBookDto.PublishedDate)
            .NotEmpty().WithMessage("Published date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Published date cannot be in the future.");

    }
}
