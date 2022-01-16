using GraphQL.Types;

namespace Server.Graph;

public class SocialSchema : Schema
{
    public SocialSchema(SocialQuery query)
    {
        Query = query;
    }
}