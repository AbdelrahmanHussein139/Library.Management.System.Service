using Library.Management.System.Application.DTOs.AuthorDTOs;
using Library.Management.System.Domain.Entities;

using Mapster;

namespace Library.Management.System.Application.Mappers;

public class AuthorMapper : TypeAdapterConfig
{
    public AuthorMapper()
    {
        // Mapping from CreateAuthorDto to Author
        TypeAdapterConfig<CreateAuthorDTO, Author>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Bio, src => src.Bio);
        // Mapping from UpdateAuthorDto to Author
        TypeAdapterConfig<UpdateAuthorDTO, Author>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Bio, src => src.Bio);
        // Mapping from Author to AuthorDto
        TypeAdapterConfig<Author, AuthorDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Bio, src => src.Bio)
            .Map(des => des.CreatedAt, src => src.CreatedAt)
            .Map(des => des.UpdatedAt, src => src.UpdatedAt);
            
    }
}
