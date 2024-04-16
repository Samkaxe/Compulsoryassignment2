using System.Diagnostics.Metrics;
using Core;
using MeasurementDatabase;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementRepository _measurementRepository;

    public MeasurementController(IMeasurementRepository measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }
    

    [HttpGet]
    public ActionResult<IEnumerable<Measurements>> GetAllMeasurements()
    {
        var measurements = _measurementRepository.GetAllMeasurements();
        return Ok(measurements);
    }

    [HttpGet("{id}")]
    public ActionResult<Measurements> GetMeasurementById(int id)
    {
        var measurement = _measurementRepository.GetMeasurementById(id);
        if (measurement == null)
        {
            return NotFound();
        }
        return Ok(measurement);
    }

    [HttpPost]
    public IActionResult AddMeasurement(Measurements measurement)
    {
        _measurementRepository.AddMeasurement(measurement);
        return CreatedAtAction(nameof(GetMeasurementById), new { id = measurement.Id }, measurement);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMeasurement(int id)
    {
        var measurementToDelete = _measurementRepository.GetMeasurementById(id);
        if (measurementToDelete == null)
        {
            return NotFound();
        }
        _measurementRepository.DeleteMeasurement(id);
        return NoContent();
    }
}