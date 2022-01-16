using Server.Models;

namespace Server.DataContext;

public class DataSeeding
{
    private readonly SocialContext _context;

    public DataSeeding(SocialContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        await _context.Database.EnsureCreatedAsync();

        if (!_context.People.Any())
        {
            var john = new Person 
            { 
                Id = 1, 
                FirstName = "John",
                LastName = "Doe",
            };
            var jane = new Person 
            { 
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
            };
            _context.People.Add(john);
            _context.People.Add(jane);

            var johnsPost = new Post
            {
                Id = 1,
                Title = "Test",
                Body = "Here's some cool info",
                Author = john,
            };
            _context.Posts.Add(johnsPost);

            var janesComment = new Comment
            {
                Content = "This is nice",
                Post = johnsPost,
                Author = jane,
            };
            _context.Comments.Add(janesComment);
            await _context.SaveChangesAsync();
        }
    }
}
