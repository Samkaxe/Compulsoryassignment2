using Core;

namespace PatientDatabase;

public class PatientRepository : IPatientRepository
{
    private readonly PatientDbContext _context;

    public PatientRepository(PatientDbContext context)
    {
        _context = context;
    }

    public List<Patient> GetAllPatients()
    {
        // _context.Database.EnsureCreated();
        return _context.Patients.ToList();
    }

    public Patient GetPatientBySSN(string ssn)
    {
        return _context.Patients.FirstOrDefault(p => p.SSN == ssn);
    }

    public void AddPatient(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
    }

    public void DeletePatient(string ssn)
    {
        var patientToDelete = _context.Patients.FirstOrDefault(p => p.SSN == ssn);
        if (patientToDelete != null)
        {
            _context.Patients.Remove(patientToDelete);
            _context.SaveChanges();
        }
    }
}