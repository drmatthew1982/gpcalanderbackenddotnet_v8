using Microsoft.EntityFrameworkCore;

namespace ClientApi.Models;

public class ClientContext : DbContext
{
    public ClientContext(DbContextOptions<ClientContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Client { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Client>()
            .HasMany(e => e.Events)
            .WithOne(e => e.Client)
            .HasForeignKey("client_id")
            .IsRequired();
    }

}