using Library.Management.System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Management.System.Domain.Entities;

public class User:BaseEntity
{
    public string FirstName { get;private set; }= string.Empty;
    public string LastName { get;private set; }= string.Empty;
    public string Email { get; private set; }= string.Empty;
    public string PasswordHash { get; private set; }=string.Empty;
    public LibrarySystemRole Role { get;private set; }=LibrarySystemRole.Guest;// e.g., Admin,Member,Guest
}
