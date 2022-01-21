using Server.DataContext;
using Server.Models;

namespace Server.DataServices;

public class PostService
{
    private readonly SocialContext _context;
    public PostService(SocialContext context)
    {
        _context = context;
    }

    public Post? GetPostById(int id)
    {
        return _context.Posts.Find(id);
    }

    public IEnumerable<Post> GetPostsForPerson(int personId)
    {
        return _context.Posts.Where(post => post.AuthorId == personId).ToArray();
    }

    public IEnumerable<Post> All()
    {
        return _context.Posts.ToArray();
    }
}