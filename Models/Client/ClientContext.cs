using Microsoft.EntityFrameworkCore;

namespace ClientApi.Models;

public class ClientContext : DbContext
{
    public ClientContext(DbContextOptions<ClientContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Event { get; set; } = null!;
}