 using System.Diagnostics;
 using Core;
 using OpenTelemetry.Trace;
 using PatientDatabase;
 using Status = OpenTelemetry.Trace.Status;

 public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;
        private readonly ActivitySource _activitySource = TelemetryActivitySource.Instance;

        public PatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAllPatients()
        {
            using var activity = _activitySource.StartActivity("GetAllPatients");
            try
            {
                return _context.Patients.ToList();
            }
            catch (Exception ex)
            {
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                throw;
            }
        }

        public Patient GetPatientBySSN(string ssn)
        {
            using var activity = _activitySource.StartActivity("GetPatientBySSN");
            try
            {
                return _context.Patients.FirstOrDefault(p => p.SSN == ssn);
            }
            catch (Exception ex)
            {
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                throw;
            }
        }

        public void AddPatient(Patient patient)
        {
            using var activity = _activitySource.StartActivity("AddPatient");
            try
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                throw;
            }
        }

        public void DeletePatient(string ssn)
        {
            using var activity = _activitySource.StartActivity("DeletePatient");
            try
            {
                var patientToDelete = _context.Patients.FirstOrDefault(p => p.SSN == ssn);
                if (patientToDelete != null)
                {
                    _context.Patients.Remove(patientToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(Status.Error.WithDescription(ex.Message));
                throw;
            }
        }
    }