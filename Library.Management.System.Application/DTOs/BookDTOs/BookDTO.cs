

namespace Library.Management.System.Application.DTOs.BookDTOs;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }=Guid.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }=DateTime.UtcNow;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
