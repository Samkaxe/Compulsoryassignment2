using System.Diagnostics.Metrics;
using Core;

namespace MeasurementDatabase;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly MeasurementDbContext _context;

    public MeasurementRepository()
    {
        _context = new MeasurementDbContext();
    }

    public List<Measurements> GetAllMeasurements()
    {
        return _context.Measurements.ToList();
    }

    public Measurements GetMeasurementById(int id)
    {
        return _context.Measurements.FirstOrDefault(m => m.Id == id);
    }

    public void AddMeasurement(Measurements measurement)
    {
        _context.Measurements.Add(measurement);
        _context.SaveChanges();
    }

    public void DeleteMeasurement(int id)
    {
        var measurementToDelete = _context.Measurements.FirstOrDefault(m => m.Id == id);
        if (measurementToDelete != null)
        {
            _context.Measurements.Remove(measurementToDelete);
            _context.SaveChanges();
        }
    }
}