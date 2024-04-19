using System.Diagnostics;

namespace MeasurementDatabase;

public static class TelemetryActivitySource
{
    public static readonly ActivitySource Instance = new ActivitySource("MeasurementService.API");
}