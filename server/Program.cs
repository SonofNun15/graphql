using Microsoft.EntityFrameworkCore;
using Server.Config;
using Server.DataContext;
using Server.Config.Graph;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SocialContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
);

builder.Services.ConfigureServices();

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

app.UseGraphQL();

app.Run();
