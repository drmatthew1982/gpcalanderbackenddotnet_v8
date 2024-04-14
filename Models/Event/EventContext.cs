using Microsoft.EntityFrameworkCore;

namespace EventApi.Models;

public class EventContext : DbContext
{
    
    public EventContext(DbContextOptions<EventContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Event { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Event>()
        .HasOne(e => e.Client)
        .WithMany(e => e.Events)
        .HasForeignKey("Client_id")
        .IsRequired(false);

     modelBuilder.Entity<Event>()
        .HasOne(e => e.Organisation)
        .WithMany(e => e.Events)
        .HasForeignKey("Org_id")
        .IsRequired(false);
}

}