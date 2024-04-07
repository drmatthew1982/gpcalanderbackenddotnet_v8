using Microsoft.EntityFrameworkCore;

namespace OrgnasitionApi.Models;

public class OrgansationContext : DbContext
{
    public OrgansationContext(DbContextOptions<OrgansationContext> options)
        : base(options)
    {
    }

    public DbSet<Organsation> Organsation { get; set; } = null!;
}