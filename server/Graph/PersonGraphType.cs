using GraphQL.Types;
using GraphQL.MicrosoftDI;
using Server.DataServices;
using Server.Models;

namespace Server.Graph;

public class PersonGraphType : ObjectGraphType<Person>
{
    public PersonGraphType()
    {
        Field<IdGraphType>("Id");
        Field(x => x.FirstName);
        Field(x => x.LastName);
        Field(x => x.Bio, nullable: true);

        Field<ListGraphType<PostGraphType>, IEnumerable<Post>>("Posts")
            .Resolve()
            .WithScope()
            .WithService<PostService>()
            .Resolve((ctx, postService) =>
            {
                return postService.GetPostsForPerson(ctx.Source.Id.Value);
            });

        Field<ListGraphType<CommentGraphType>, IEnumerable<Comment>>("Comments")
            .Resolve()
            .WithScope()
            .WithService<CommentService>()
            .Resolve((ctx, commentService) => 
            {
                return commentService.GetCommentsForPerson(ctx.Source.Id.Value);
            });
    }
}