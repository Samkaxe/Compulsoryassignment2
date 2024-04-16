using Core;

namespace PatientDatabase;

public interface IPatientRepository
{
    List<Patient> GetAllPatients();
    Patient GetPatientBySSN(string ssn);
    void AddPatient(Patient patient);
    void DeletePatient(string ssn);
}