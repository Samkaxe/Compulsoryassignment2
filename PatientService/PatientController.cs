using Core;
using Microsoft.AspNetCore.Mvc;
using PatientDatabase;
using Status = OpenTelemetry.Trace.Status;

namespace PatientService;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet]
    [HttpGet]
    public ActionResult<IEnumerable<Patient>> GetAllPatients()
    {
        using var activity = TelemetryActivitySource.Instance.StartActivity("PatientService.API");
        try
        {
            var patients = _patientRepository.GetAllPatients();
            return Ok(patients);
        }
        catch (Exception ex)
        {
           // activity?.SetStatus(Status.Error.WithDescription(ex.Message));
            throw;
        }
        finally
        {
            activity?.Stop();
        }
    }

    [HttpGet("{ssn}")]
    public ActionResult<Patient> GetPatientBySSN(string ssn)
    {
        var patient = _patientRepository.GetPatientBySSN(ssn);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost]
    public IActionResult AddPatient(Patient patient)
    {
        _patientRepository.AddPatient(patient);
        return CreatedAtAction(nameof(GetPatientBySSN), new { ssn = patient.SSN }, patient);
    }

    [HttpDelete("{ssn}")]
    public IActionResult DeletePatient(string ssn)
    {
        var patientToDelete = _patientRepository.GetPatientBySSN(ssn);
        if (patientToDelete == null)
        {
            return NotFound();
        }
        _patientRepository.DeletePatient(ssn);
        return NoContent();
    }
}