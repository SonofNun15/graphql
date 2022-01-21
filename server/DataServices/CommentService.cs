using Server.DataContext;
using Server.Models;

namespace Server.DataServices;

public class CommentService
{
    private readonly SocialContext _context;
    public CommentService(SocialContext context)
    {
        _context = context;
    }

    public IEnumerable<Comment> GetCommentsForPerson(int personId)
    {
        return _context.Comments.Where(c => c.AuthorId == personId).ToArray();
    }

    public IEnumerable<Comment> GetCommentsForPost(int postId)
    {
        return _context.Comments.Where(c => c.PostId == postId).ToArray();
    }
}