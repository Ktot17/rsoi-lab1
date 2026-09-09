using DBComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace DBComponent.Postgres;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext() {}
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) {}

    public virtual DbSet<PersonDb> People { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<PersonDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasColumnType("text");
            entity.Property(e => e.Age);
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Work).HasColumnType("text");
        });
    }
}