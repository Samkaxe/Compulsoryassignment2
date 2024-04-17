using Core;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PatientDatabase;

namespace Tests;

public class PatientRepositoryTests
{
    [Fact]
    public void GetAllPatients_ReturnsAllPatientsFromContext()
    {
        var mockData = new List<Patient>
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

        var mockContext = new Mock<PatientDbContext>();
        mockContext.Setup(context => context.Patients)
            .ReturnsDbSet(mockData);

        var repository = new PatientRepository(mockContext.Object);

        var patients = repository.GetAllPatients();
        
        Assert.Equal(mockData,patients);
    }
    
    [Fact]
    public void GetPatientBySSN_ReturnsPatientWithCorrectSSN()
    {
        // Arrange
        var ssn = "AAA-GG-SSSS";
        var mockData = new List<Patient>
        {
            new Patient { Email = "bobby@email.com", Measurements = new List<Measurements>(), Name = "Bob", SSN = ssn },
            new Patient { Email = "bobby@hotmail.com", Measurements = new List<Measurements>(), Name = "Bob's 2nd degree brother", SSN = "YSG-AF-1234" }
        };

        var mockContext = new Mock<PatientDbContext>();
        mockContext.Setup(c => c.Patients).ReturnsDbSet(mockData);

        var repository = new PatientRepository(mockContext.Object);

        // Act
        var result = repository.GetPatientBySSN(ssn);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ssn, result.SSN);
    }
    
    [Fact]
    public void AddPatient_AddsPatientToContext()
    {
        // Arrange
        var patient = new Patient { Email = "new@email.com", Name = "New Patient", SSN = "NEW-SSN-000" };
        var mockContext = new Mock<PatientDbContext>();
        var mockSet = new Mock<DbSet<Patient>>();
        mockContext.Setup(m => m.Patients).Returns(mockSet.Object);

        var repository = new PatientRepository(mockContext.Object);

        // Act
        repository.AddPatient(patient);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<Patient>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
    
    [Fact]
    public void DeletePatient_DeletesCorrectPatient()
    {
        // Arrange
        var ssnToDelete = "AAA-GG-SSSS";
        var patients = new List<Patient>
        {
            new Patient { Name = "Bob", SSN = ssnToDelete },
            new Patient { Name = "Alice", SSN = "BBB-HH-SSSS" }
        };

        var mockContext = new Mock<PatientDbContext>();
        var mockSet = new Mock<DbSet<Patient>>();
        mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.AsQueryable().Provider);
        mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.AsQueryable().Expression);
        mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.AsQueryable().ElementType);
        mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        mockContext.Setup(c => c.Patients).Returns(mockSet.Object);

        var repository = new PatientRepository(mockContext.Object);

        // Act
        repository.DeletePatient(ssnToDelete);

        // Assert
        mockSet.Verify(m => m.Remove(It.Is<Patient>(p => p.SSN == ssnToDelete)), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
}