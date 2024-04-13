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
        .HasForeignKey("client_id")
        .IsRequired();

     modelBuilder.Entity<Event>()
        .HasOne(e => e.Organisation)
        .WithMany(e => e.Events)
        .HasForeignKey("org_id")
        .IsRequired();
}

}