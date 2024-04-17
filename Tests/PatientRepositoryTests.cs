using Core;
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

        // var repository = new PatientC
    }
}