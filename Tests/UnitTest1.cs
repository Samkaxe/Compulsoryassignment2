using Core;
using MeasurementDatabase;
using MeasurementService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientDatabase;
using PatientService;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void GetAllPatients_ReturnsListOfPatients()
    {
        // Arrange
        var mockRepository = new Mock<IPatientRepository>(); 
        mockRepository.Setup(repo => repo.GetAllPatients())
            .Returns(new List<Patient> { new Patient(), new Patient() });

        var controller = new PatientController(mockRepository.Object);

        // Act
        var result = controller.GetAllPatients();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var patients = Assert.IsAssignableFrom<IEnumerable<Patient>>(okResult.Value);
        Assert.NotEmpty(patients);
    }
    
    [Fact]
    public void GetAllMeasurements_ReturnsListOfMeasurements()
    {
        // Arrange
        var mockRepository = new Mock<IMeasurementRepository>(); 
        mockRepository.Setup(repo => repo.GetAllMeasurements())
            .Returns(new List<Measurements> { new Measurements(), new Measurements() });

        var controller = new MeasurementController(mockRepository.Object);

        // Act
        var result = controller.GetAllMeasurements();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var measurements = Assert.IsAssignableFrom<IEnumerable<Measurements>>(okResult.Value);
        Assert.NotEmpty(measurements);
    }
}