using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientDatabase;
using PatientService;
using PatientController = PatientService.PatientController;

namespace Tests;

public class PatientControllerTests
{
    [Fact]
    public void GetAllPatients_ReturnsListOfPatients()
    {
        List<Patient> mockList = new List<Patient>
        {
            new Patient
            {
                Email = "bobby@email.com",
                Measurements = new List<Measurements>(),
                Name = "Bob",
                SSN = "AAA-GG-SSSS"
            },
            new Patient
            {
                Email = "bobby@hotmail.com",
                Measurements = new List<Measurements>(),
                Name = "Bob's 2nd degree brother",
                SSN = "YSG-AF-1234"
            },
            new Patient
            {
                Email = "paperboi@email.com",
                Measurements = new List<Measurements>(),
                Name = "Alfred",
                SSN = "AAA-GG-SSSS"
            }
        };
        
        var mockRepo = new Mock<IPatientRepository>();
        mockRepo.Setup(repo => repo.GetAllPatients())
            .Returns(mockList);

        var controller = new PatientController(mockRepo.Object);

        var result = controller.GetAllPatients();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var patients = Assert.IsAssignableFrom<IEnumerable<Patient>>(okResult.Value);
        Assert.NotEmpty(patients);
        Assert.Equal(3, patients.Count());
    }
    
    [Fact]
    public void GetPatientBySSN_ExistingPatient_ReturnsPatient()
    {
        // Arrange
        var mockSSN = "AAA-GG-SSSS"; 
        var expectedPatient = new Patient { SSN = mockSSN, Name = "Bob" }; // Match your data 
        var mockRepo = new Mock<IPatientRepository>();
        mockRepo.Setup(r => r.GetPatientBySSN(mockSSN)).Returns(expectedPatient);
        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = controller.GetPatientBySSN(mockSSN);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPatient = Assert.IsType<Patient>(okResult.Value);
        Assert.Equal(expectedPatient, returnedPatient);  // Check all relevant properties
    }
    
    [Fact]
    public void AddPatient_ValidPatient_ReturnsCreatedAtAction()
    {
        // Arrange
        var newPatient = new Patient { SSN = "NEW-AA-1111", Name = "Alice" }; // Your data
        var mockRepo = new Mock<IPatientRepository>();
        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = controller.AddPatient(newPatient);

        // Assert
        mockRepo.Verify(r => r.AddPatient(newPatient), Times.Once); // Check repository call
    }
    
    [Fact]
    public void DeletePatient_ExistingPatient_ReturnsNoContent()
    {
        // Arrange
        var mockSSN = "AAA-GG-SSSS";
        var mockRepo = new Mock<IPatientRepository>();
        mockRepo.Setup(r => r.GetPatientBySSN(mockSSN)).Returns(new Patient()); // Patient exists
        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = controller.DeletePatient(mockSSN);

        // Assert
        mockRepo.Verify(r => r.DeletePatient(mockSSN), Times.Once); 
    }
}