using Library.Management.System.Domain.Enums;

namespace Library.Management.System.Application.DTOs.UserDTOs;

public class UserDTO
{
    public Guid Id { get; set; } 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public LibrarySystemRole Role { get; set; } = LibrarySystemRole.Guest;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
