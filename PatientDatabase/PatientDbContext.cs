using Core;
using Microsoft.EntityFrameworkCore;

namespace PatientDatabase;

public class PatientDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
       
        optionsBuilder.UseSqlite("Data Source=../PatientDatabase/patient.db");
        //optionsBuilder.UseSqlite("Data Source=C:\\Users\\charl\\RiderProjects\\Compulsoryassignment2\\PatientDatabase\\patient.db");
    }
}