using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.DataContext;

public class SocialContext : DbContext
{
    public SocialContext(DbContextOptions<SocialContext> options)
        : base(options)
    {}

    public DbSet<Person> People => Set<Person>();

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<Comment> Comments => Set<Comment>();
}
