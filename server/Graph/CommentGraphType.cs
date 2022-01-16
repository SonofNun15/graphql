using GraphQL.Types;
using Server.Models;

namespace Server.Graph;

public class CommentGraphType : ObjectGraphType<Comment>
{
    public CommentGraphType()
    {
        Field( x=> x.Id);
        Field(x => x.Content);
        Field(x => x.Author);
        Field(x => x.Post);
    }
}