using Microsoft.EntityFrameworkCore;

namespace OrganisitionApi.Models;

public class OrganisationContext : DbContext
{
    public OrganisationContext(DbContextOptions<OrganisationContext> options)
        : base(options)
    {
    }

    public DbSet<Organisation> Organisation { get; set; } = null!;
}