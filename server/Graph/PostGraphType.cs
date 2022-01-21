using GraphQL.Types;
using GraphQL.MicrosoftDI;
using Server.Models;
using Server.DataServices;

namespace Server.Graph;

public class PostGraphType : ObjectGraphType<Post>
{
    public PostGraphType()
    {
        Field<IdGraphType>("Id");
        Field(x => x.Title);
        Field(x => x.Body);

        Field<PersonGraphType, Person>("Author")
            .Resolve()
            .WithScope()
            .WithService<PersonService>()
            .Resolve((ctx, personService) => 
            {
                return personService.GetPersonById(ctx.Source.AuthorId);
            });

        Field<ListGraphType<CommentGraphType>, IEnumerable<Comment>>("Comments")
            .Resolve()
            .WithScope()
            .WithService<CommentService>()
            .Resolve((ctx, commentService) => 
            {
                return commentService.GetCommentsForPost(ctx.Source.Id.Value);
            });
    }
}