using GraphQL;
using GraphQL.Execution;
// using GraphQL.DataLoader;
using GraphQL.Instrumentation;
using GraphQL.Server;
using GraphQL.SystemReactive;
using Microsoft.EntityFrameworkCore;
using Server.Config;
using Server.DataContext;
using Server.Graph;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SocialContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
);

builder.Services.ConfigureServices();

var graphBuilder = GraphQL.MicrosoftDI.GraphQLBuilderExtensions.AddGraphQL(builder.Services)
    .AddSubscriptionDocumentExecuter()
    .AddServer(true)
    .AddSchema<SocialSchema>()
    .ConfigureExecution(options =>
    {
        options.EnableMetrics = builder.Environment.IsDevelopment();
        // var logger = options.RequestServices.GetRequiredService<ILogger<Startup>>();
        // options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
    })
    .AddSystemTextJson()
    .Configure<ErrorInfoProviderOptions>(opt => opt.ExposeExceptionStackTrace = builder.Environment.IsDevelopment())
    // .AddDataLoader()
    // .AddWebSockets()
    .AddGraphTypes(typeof(SocialSchema).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dataSeeding = scope.ServiceProvider.GetRequiredService<DataSeeding>();
        await dataSeeding.Seed();
    }

    app.UseGraphQLPlayground("/graphql/playground");
}

app.UseGraphQL<SocialSchema>();

app.Run();
