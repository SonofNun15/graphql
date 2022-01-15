using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class Comment
{
    [Key]
    public int? Id { get; set; }
    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    [Required]
    public int AuthorId { get; set; }
    public Person Author { get; set; } = null!;
}