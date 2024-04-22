using System.Diagnostics;
using Core;
using Microsoft.AspNetCore.Mvc;
using PatientDatabase;
using Serilog;
using Status = OpenTelemetry.Trace.Status;

namespace PatientService;

[ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ActivitySource _activitySource = TelemetryActivitySource.Instance;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Patient>> GetAllPatients()
        {
            using var activity = _activitySource.StartActivity("GetAllPatients");
            try
            {
                var patients = _patientRepository.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing GetAllPatients");
                throw;
            }
        }

        [HttpGet("{ssn}")]
        public ActionResult<Patient> GetPatientBySSN(string ssn)
        {
            using var activity = _activitySource.StartActivity("GetPatientBySSN");
            try
            {
                var patient = _patientRepository.GetPatientBySSN(ssn);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing GetPatientBySSN for SSN: {SSN}", ssn);
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            using var activity = _activitySource.StartActivity("AddPatient");
            try
            {
                _patientRepository.AddPatient(patient);
                return CreatedAtAction(nameof(GetPatientBySSN), new { ssn = patient.SSN }, patient);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing AddPatient");
                throw;
            }
        }

        [HttpDelete("{ssn}")]
        public IActionResult DeletePatient(string ssn)
        {
            using var activity = _activitySource.StartActivity("DeletePatient");
            try
            {
                var patientToDelete = _patientRepository.GetPatientBySSN(ssn);
                if (patientToDelete == null)
                {
                    return NotFound();
                }
                _patientRepository.DeletePatient(ssn);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing DeletePatient for SSN: {SSN}", ssn);
                throw;
            }
        }
    }