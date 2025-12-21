using Library.Management.System.Application.DTOs.LoanDTOs;
using Library.Management.System.Domain.Entities;

using Mapster;

namespace Library.Management.System.Application.Mappers;

public class LoanMapper : TypeAdapterConfig
{
    public LoanMapper()
    {
        // Mapping from CreateLoanDto to Loan
        TypeAdapterConfig<CreateLoanDTO, Loan>.NewConfig()
            .Map(dest => dest.LoanDate, src => src.LoanDate)
            .Map(dest => dest.ReturnDate, src => src.ReturnDate)
            .Map(dest => dest.BookId, src => src.BookId)
            .Map(dest => dest.BorrowerId, src => src.BorrowerId);
        // Mapping from UpdateLoanDto to Loan
        TypeAdapterConfig<UpdateLoanDTO, Loan>.NewConfig()
            .Map(dest => dest.LoanDate, src => src.LoanDate)
            .Map(dest => dest.ReturnDate, src => src.ReturnDate)
            .Map(dest => dest.BookId, src => src.BookId)
            .Map(dest => dest.BorrowerId, src => src.BorrowerId);
        // Mapping from Loan to LoanDto
        TypeAdapterConfig<Loan, LoanDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.LoanDate, src => src.LoanDate)
            .Map(dest => dest.ReturnDate, src => src.ReturnDate)
            .Map(dest => dest.BookId, src => src.BookId)
            .Map(dest => dest.BorrowerId, src => src.BorrowerId)
            .Map(des => des.CreatedAt, src => src.CreatedAt)
            .Map(des => des.UpdatedAt, src => src.UpdatedAt);
            
    }
}
