using Library.Management.System.Application.DTOs.UserDTOs;
using Library.Management.System.Domain.Entities;

using Mapster;

namespace Library.Management.System.Application.Mappers;

public class UserMapper : TypeAdapterConfig
{
    public UserMapper()
    {
        // Mapping from CreateUserDto to User
        TypeAdapterConfig<CreateUserDTO, User>.NewConfig()
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.PasswordHash)
            .Map(dest => dest.Role, src => src.Role);
        // Mapping from UpdateUserDto to User
        TypeAdapterConfig<UpdateUserDTO, User>.NewConfig()
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.PasswordHash)
            .Map(dest => dest.Role, src => src.Role);
        // Mapping from User to UserDto
        TypeAdapterConfig<User, UserDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role)
            .Map(des => des.CreatedAt, src => src.CreatedAt)
            .Map(des => des.UpdatedAt, src => src.UpdatedAt);
            
    }
}
