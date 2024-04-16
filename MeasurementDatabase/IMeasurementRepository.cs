using Core;

namespace MeasurementDatabase;

public interface IMeasurementRepository
{
    List<Measurements> GetAllMeasurements();
    Measurements GetMeasurementById(int id);
    void AddMeasurement(Measurements measurement);
    void DeleteMeasurement(int id);
}