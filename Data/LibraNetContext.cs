using LibraNet.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Data;

public class LibraNetContext : DbContext
{
    public LibraNetContext(DbContextOptions<LibraNetContext> options) : base(options)
    { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<Media> MediaFormats { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
