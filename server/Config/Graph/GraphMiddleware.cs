using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.Options;
using Server.Config.Serialization;

namespace Server.Config.Graph;

public class GraphMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDocumentExecuter _executor;
    private readonly IDocumentWriter _writer;

    private readonly ISerializer _serializer;
    private readonly GraphOptions _options;

    public GraphMiddleware(
        RequestDelegate next, 
        IDocumentWriter writer, 
        IDocumentExecuter executor, 
        ISerializer serializer,
        IOptions<GraphOptions> options)
    {
        _next = next;
        _executor = executor;
        _writer = writer;
        _serializer = serializer;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context, ISchema schema)
    {
        if (IsGraphEndpoint(context.Request.Path) && IsGraphVerb(context.Request.Method))
        {
            var request = await GetGraphRequest(context.Request.Body);

            if (request == null)
            {
                context.Response.StatusCode = 400;
            }
            else
            {
                await HandleGraphRequest(context, schema, request);
            }
        }
        else
        {
            await _next(context);
        }
    }

    private bool IsGraphEndpoint(PathString path)
    {
        return path.StartsWithSegments(_options.Endpoint);
    }

    private bool IsGraphVerb(string requestMethod)
    {
        return _options.HttpVerbs.Any(verb => 
            string.Equals(requestMethod, verb, StringComparison.OrdinalIgnoreCase)
        );
    }

    private Task<GraphRequest?> GetGraphRequest(Stream requestBody)
    {
        return _serializer.DeserializeAsync<GraphRequest>(requestBody);
    }

    private async Task HandleGraphRequest(HttpContext context, ISchema schema, GraphRequest graphRequest)
    {
        var result = await _executor.ExecuteAsync(options =>
        {
            options.Query = graphRequest.Query;
            options.Schema = schema;
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 200;

        await _writer.WriteAsync(context.Response.Body, result);
    }
}