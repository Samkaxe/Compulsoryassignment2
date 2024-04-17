using Core;
using Status = OpenTelemetry.Trace.Status;

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
        using var activity = TelemetryActivitySource.Instance.StartActivity("PatientRepository.GET-Method");
        try
        {
            return _context.Patients.ToList();
        }
        catch (Exception ex)
        {
            //   activity?.SetStatus(Status.Error.WithDescription(ex.Message));
            throw;
        }
        finally
        {
            activity?.Stop();
        }
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