using GraphQL;
using Server.Config.Serialization;
using Server.DataContext;
using Server.DataServices;
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
        services.AddScoped<IServiceProvider>(
            s => new FuncServiceProvider(s.GetRequiredService)
        );

        services.AddSingleton<ISerializer, Json>();
        services.AddScoped<DataSeeding>();
    }

    private static void ConfigureGraph(IServiceCollection services)
    {
        services.AddScoped<PersonService>();
        services.AddScoped<PostService>();
        services.AddScoped<CommentService>();
    }
}