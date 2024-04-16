using System.Diagnostics.Metrics;
using Core;
using Microsoft.EntityFrameworkCore;

namespace MeasurementDatabase;

public class MeasurementDbContext : DbContext
{
    public DbSet<Measurements> Measurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=measurement.db");
    }
}