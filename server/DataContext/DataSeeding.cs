using Server.Models;

namespace Server.DataContext;

public static class DataSeeding
{
    public static void Seed(SocialContext context)
    {
        context.Database.EnsureCreated();

        if (!context.People.Any())
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
            context.People.Add(john);
            context.People.Add(jane);

            var johnsPost = new Post
            {
                Id = 1,
                Title = "Test",
                Body = "Here's some cool info",
                Author = john,
            };
            context.Posts.Add(johnsPost);

            var janesComment = new Comment
            {
                Content = "This is nice",
                Post = johnsPost,
                Author = jane,
            };
            context.Comments.Add(janesComment);
            context.SaveChanges();
        }
    }
}
