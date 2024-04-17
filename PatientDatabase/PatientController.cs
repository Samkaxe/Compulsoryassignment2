using Core;

namespace PatientDatabase;

public class PatientController : IPatientRepository
{
    private readonly PatientDbContext _context;

    public PatientController()
    {
        _context = new PatientDbContext();
    }

    public List<Patient> GetAllPatients()
    {
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