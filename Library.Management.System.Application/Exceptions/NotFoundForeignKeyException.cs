namespace Library.Management.System.Application.Exceptions;

public class NotFoundForeignKeyException : Exception
{
    public NotFoundForeignKeyException(string entityName, Guid id)
        : base($"{entityName} with ID {id} was not found.") { }
}
