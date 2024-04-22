using System.Diagnostics;
using System.Diagnostics.Metrics;
using Core;
using MeasurementDatabase;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MeasurementService;
[ApiController]
    [Route("api/[controller]")]
    public class MeasurementController : ControllerBase
    {
        
        private readonly IMeasurementRepository _measurementRepository;
        private readonly ActivitySource _activitySource = TelemetryActivitySource.Instance;

        public MeasurementController(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Measurements>> GetAllMeasurements()
        {
            Log.Warning("Getting all measurements");
            using var activity = _activitySource.StartActivity("GetAllMeasurements");
            try
            {
                var measurements = _measurementRepository.GetAllMeasurements();
                return Ok(measurements);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing GetAllMeasurements");
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Measurements> GetMeasurementById(int id)
        {
            using var activity = _activitySource.StartActivity("GetMeasurementById");
            try
            {
                var measurement = _measurementRepository.GetMeasurementById(id);
                if (measurement == null)
                {
                    return NotFound();
                }
                return Ok(measurement);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing GetMeasurementById with id {Id}", id);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMeasurement(Measurements measurement)
        {
            using var activity = _activitySource.StartActivity("AddMeasurement");
            try
            {
                await _measurementRepository.AddMeasurement(measurement);
                return CreatedAtAction(nameof(GetMeasurementById), new { id = measurement.Id }, measurement);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing AddMeasurement");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeasurement(int id)
        {
            using var activity = _activitySource.StartActivity("DeleteMeasurement");
            try
            {
                var measurementToDelete = _measurementRepository.GetMeasurementById(id);
                if (measurementToDelete == null)
                {
                    return NotFound();
                }
                _measurementRepository.DeleteMeasurement(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing DeleteMeasurement with id {Id}", id);
                throw;
            }
        }
    }