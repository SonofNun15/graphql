using Microsoft.EntityFrameworkCore;
using Server.DataContext;

var builder = WebApplication.CreateBuilder(args);

var cStr = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<SocialContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope()) {
        var context = scope.ServiceProvider.GetRequiredService<SocialContext>();
        DataSeeding.Seed(context);
    }
}

app.MapGet("/", () => "Hello World!");

app.Run();
