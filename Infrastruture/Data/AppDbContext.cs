using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

    public DbSet<Author> Authors {get;set;}
    public DbSet<Book> Books {get;set;}
    public DbSet<Genre> Genres {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}