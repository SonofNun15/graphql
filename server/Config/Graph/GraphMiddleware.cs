using System.Text.Json;
using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.Extensions.Options;

namespace Server.Config.Graph;

public class GraphMiddleware
{
    private readonly RequestDelegate _next;
    private readonly GraphOptions _options;

    public GraphMiddleware(RequestDelegate next,  IOptions<GraphOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public Task InvokeAsync(HttpContext context, ISchema schema)
    {
        if (IsGraphEndpoint(context.Request.Path) && IsGraphVerb(context.Request.Method))
        {
            return HandleGraphRequest(context, schema);
        }
        else
        {
            return _next(context);
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

    private async Task HandleGraphRequest(HttpContext context, ISchema schema)
    {
        var request = await JsonSerializer.DeserializeAsync<GraphRequest>(
            context.Request.Body,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        var result = await schema.ExecuteAsync(options => 
        {
            options.Query = request?.Query;
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 200;

        var writer = new StreamWriter(context.Response.Body);
        await writer.WriteAsync(result);
        await writer.FlushAsync();
    }
}