using Library.Management.System.Domain.Entities;
using Library.Management.System.Application.DTOs.BookDTOs;
using Mapster;

namespace Library.Management.System.Application.Mappers;

public class BookMapper : TypeAdapterConfig
{
    public BookMapper()
    {
        // Mapping from CreateBookDTO to Book
        TypeAdapterConfig<CreateBookDTO, Book>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.ISBN, src => src.ISBN)
            .Map(dest => dest.PublishedDate, src => src.PublishedDate);
        // Mapping from UpdateBookDto to Book
        TypeAdapterConfig<UpdateBookDTO, Book>.NewConfig()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.ISBN, src => src.ISBN)
            .Map(dest => dest.PublishedDate, src => src.PublishedDate);
        // Mapping from Book to BookDto
        TypeAdapterConfig<Book, BookDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Map(dest => dest.ISBN, src => src.ISBN)
            .Map(dest => dest.PublishedDate, src => src.PublishedDate)
            .Map(des => des.CreatedAt, src => src.CreatedAt)
            .Map(des => des.UpdatedAt, src => src.UpdatedAt);
            
    }
}
