using GraphQL.Types;
using Server.Models;

namespace Server.Graph;

public class PostGraphType : ObjectGraphType<Post>
{
    public PostGraphType()
    {
        Field(x => x.Id);
        Field(x => x.Title);
        Field(x => x.Body);
        Field(x => x.Author);
        Field(x => x.Comments);
    }
}