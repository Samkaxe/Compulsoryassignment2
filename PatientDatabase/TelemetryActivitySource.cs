using System.Diagnostics;

namespace PatientDatabase;

public static class TelemetryActivitySource
{
    public static readonly ActivitySource Instance = new ActivitySource("PatientService.API");
}