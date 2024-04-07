using Microsoft.EntityFrameworkCore;

namespace MedicalRecordApi.Models;

public class MedicalRecordContext : DbContext
{
    public MedicalRecordContext(DbContextOptions<MedicalRecordContext> options)
        : base(options)
    {
    }

    public DbSet<MedicalRecord> Event { get; set; } = null!;
}