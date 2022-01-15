using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class Post
{
    [Key]
    public int? Id { get; set; }
    public string? Title { get; set; }
    [Required]
    public string Body { get; set; } = null!;
    [Required]
    public DateTime PostDate { get; set; } = DateTime.UtcNow;

    [Required]
    public int AuthorId { get; set; }
    public Person Author { get; set; } = null!;

    public IList<Comment> Comments { get; set; } = new List<Comment>();
}