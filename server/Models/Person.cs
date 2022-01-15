using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class Person
{
    [Key]
    public int? Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public string? Bio { get; set; }

    public IList<Post> Posts { get; set; } = new List<Post>();
    public IList<Comment> Commands { get; set; } = new List<Comment>();
}