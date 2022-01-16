namespace Server.Config.Graph;

public class GraphOptions
{
    public string Endpoint { get; set; } = "/graphql";
    public IEnumerable<string> HttpVerbs { get; set; } = new string[] { "POST" };
}