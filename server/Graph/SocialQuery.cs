using GraphQL.Types;
using Server.DataContext;

namespace Server.Graph;

public class SocialQuery : ObjectGraphType
{
    public SocialQuery(SocialContext db)
    {
        Field<StringGraphType>(
            name: "hello",
            resolve: context => "world"
        );
        Field<StringGraphType>(
            name: "howdy",
            resolve: context => "universe"
        ); 
        // Field<PersonGraphType>(
        //     "people",
        //     resolve: context => db.People
        // );

        // Field<PostGraphType>(
        //     "posts",
        //     resolve: context => db.Posts
        // );
    }
}