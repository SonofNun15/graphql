namespace Server.Config.Graph;

public static class GraphMiddlewareExtensions
{
    public static IApplicationBuilder UseGraphQL(this WebApplication builder)
    {
        return builder.UseMiddleware<GraphMiddleware>();
    }

    public static IServiceCollection AddGraphQL(this IServiceCollection services, Action<GraphOptions> action)
    {
        return services.Configure(action);
    }
}