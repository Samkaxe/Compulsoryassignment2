using System.Diagnostics;
using System.Diagnostics.Metrics;
using Core;
using Status = OpenTelemetry.Trace.Status;

namespace MeasurementDatabase;

public class MeasurementRepository : IMeasurementRepository
    {
        private readonly MeasurementDbContext _context;
        private readonly ActivitySource _activitySource = TelemetryActivitySource.Instance;

        public MeasurementRepository(MeasurementDbContext context)
        {
            _context = context;
        }

        public List<Measurements> GetAllMeasurements()
        {
            using var activity = _activitySource.StartActivity("GetAllMeasurements");
            try
            {
                return _context.Measurements.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Measurements GetMeasurementById(int id)
        {
            using var activity = _activitySource.StartActivity("GetMeasurementById");
            try
            {
                return _context.Measurements.FirstOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddMeasurement(Measurements measurement)
        {
            using var activity = _activitySource.StartActivity("AddMeasurement");
            try
            {
                _context.Measurements.Add(measurement);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteMeasurement(int id)
        {
            using var activity = _activitySource.StartActivity("DeleteMeasurement");
            try
            {
                var measurementToDelete = _context.Measurements.FirstOrDefault(m => m.Id == id);
                if (measurementToDelete != null)
                {
                    _context.Measurements.Remove(measurementToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }