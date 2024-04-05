using Microsoft.EntityFrameworkCore;

namespace EventApi.Models;

public class EventContext : DbContext
{
    public EventContext(DbContextOptions<EventContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Event { get; set; } = null!;
}