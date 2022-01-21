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
            var debby = new Person
            {
                Id = 3,
                FirstName = "Debby",
                LastName = "Jones",
            };
            _context.People.Add(john);
            _context.People.Add(jane);
            _context.People.Add(debby);

            var johnsPost = new Post
            {
                Id = 1,
                Title = "Test",
                Body = "Here's some cool info",
                Author = john,
            };
            var debbysPost = new Post
            {
                Id = 2,
                Title = "GraphQL!",
                Body = "GraphQL is pretty cool. Let's see what it does well and what it doesn't.",
                Author = debby,
            };
            _context.Posts.Add(johnsPost);
            _context.Posts.Add(debbysPost);

            var janesComment = new Comment
            {
                Id = 1,
                Content = "This is nice",
                Post = johnsPost,
                Author = jane,
            };
            var johnsComment = new Comment
            {
                Id = 2,
                Content = "I mean, I think it's pretty cool...",
                Post = debbysPost,
                Author = john,
            };
            _context.Comments.Add(janesComment);
            _context.Comments.Add(johnsComment);
            await _context.SaveChangesAsync();
        }
    }
}
