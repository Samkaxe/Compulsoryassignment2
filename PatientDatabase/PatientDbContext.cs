using Core;
using Microsoft.EntityFrameworkCore;

namespace PatientDatabase;

public class PatientDbContext : DbContext
{
    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlite("Data Source=patient.db");
    }
}