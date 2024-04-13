using Microsoft.EntityFrameworkCore;

namespace OrganisitionApi.Models;

public class OrganisationContext : DbContext
{
    public OrganisationContext(DbContextOptions<OrganisationContext> options)
        : base(options)
    {
    }

    public DbSet<Organisation> Organisation { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder){
    modelBuilder.Entity<Organisation>()
        .HasMany(e => e.Events)
        .WithOne(e => e.Organisation)
        .HasForeignKey("org_id")
        .IsRequired(false);
    }
}