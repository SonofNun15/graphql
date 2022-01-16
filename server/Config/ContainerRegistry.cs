using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Server.Config.Serialization;
using Server.DataContext;
using Server.Graph;

namespace Server.Config;

public static class ContainerRegistry
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        ConfigureGeneral(services);
        ConfigureGraph(services);
    }

    private static void ConfigureGeneral(IServiceCollection services)
    {
        services.AddSingleton<ISerializer, Json>();
        services.AddScoped<DataSeeding>();
    }

    private static void ConfigureGraph(IServiceCollection services)
    {
        services.AddSingleton<IDocumentWriter, DocumentWriter>();
        services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        services.AddScoped<SocialQuery>();
        services.AddScoped<ISchema, SocialSchema>();
    }
}