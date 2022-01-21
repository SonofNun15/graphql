using GraphQL.Types;
using GraphQL.MicrosoftDI;
using Server.Models;
using Server.DataServices;

namespace Server.Graph;

public class CommentGraphType : ObjectGraphType<Comment>
{
    public CommentGraphType()
    {
        Field<IdGraphType>("Id");
        Field(x => x.Content);

        Field<PersonGraphType, Person>("Author")
            .Resolve()
            .WithScope()
            .WithService<PersonService>()
            .Resolve((ctx, personService) => 
            {
                return personService.GetPersonById(ctx.Source.AuthorId);
            });

        Field<PostGraphType, Post>("Post")
            .Resolve()
            .WithScope()
            .WithService<PostService>()
            .Resolve((ctx, postService) => 
            {
                return postService.GetPostById(ctx.Source.PostId);
            });
    }
}