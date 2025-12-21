using Library.Management.System.Application.DTOs.BorrowerDTOs;
using Library.Management.System.Domain.Entities;

using Mapster;

namespace Library.Management.System.Application.Mappers;

public class BorrowerMapper : TypeAdapterConfig
{
    public BorrowerMapper()
    {
        // Mapping from CreateBorrowerDto to Borrower
        TypeAdapterConfig<CreateBorrowerDTO, Borrower>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
        // Mapping from UpdateBorrowerDTO to Borrower
        TypeAdapterConfig<UpdateBorrowerDTO, Borrower>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
        // Mapping from Borrower to BorrowerDTO
        TypeAdapterConfig<Borrower, BorrowerDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(des => des.CreatedAt, src => src.CreatedAt)
            .Map(des => des.UpdatedAt, src => src.UpdatedAt);

    }
}
