using GraphQL.Types;
using Server.DataServices;
using GraphQL.MicrosoftDI;
using Server.Models;

namespace Server.Graph;

public class SocialQuery : ObjectGraphType
{
    public SocialQuery()
    {
        Field<PersonGraphType, Person>("person")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .Resolve()
            .WithScope()
            .WithService<PersonService>()
            .Resolve((ctx, personService) => 
            {
                int id = (int) ctx.Arguments["id"].Value;
                return personService.GetPersonById(id);
            });

        Field<ListGraphType<PersonGraphType>, IEnumerable<Person>>("people")
            .Resolve()
            .WithScope()
            .WithService<PersonService>()
            .Resolve((ctx, personService) =>
            {
                return personService.All();
            });

        Field<PostGraphType, Post>("post")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .Resolve()
            .WithScope()
            .WithService<PostService>()
            .Resolve((ctx, postService) => {
                int id = (int) ctx.Arguments["id"].Value;
                return postService.GetPostById(id);
            });
    }
}