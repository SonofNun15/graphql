using GraphQL.Types;
using Server.Models;

namespace Server.Graph;

public class PersonGraphType : ObjectGraphType<Person>
{
    public PersonGraphType()
    {
        Field(x => x.Id);
        Field(x => x.FirstName);
        Field(x => x.LastName);
        Field(x => x.Bio);
        Field(x => x.Posts);
        Field(x => x.Comments);
    }
}