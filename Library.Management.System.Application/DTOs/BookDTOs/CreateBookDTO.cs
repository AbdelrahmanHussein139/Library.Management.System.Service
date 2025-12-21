
namespace Library.Management.System.Application.DTOs.BookDTOs;
public class CreateBookDTO
{
    public string Title { get; set; } = string.Empty;
    public Guid AuthorId { get; set; } = Guid.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
}
