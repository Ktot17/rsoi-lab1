using AutoFixture;
using AutoFixture.Xunit2;
using BLComponent;
using BLComponent.InputPorts;
using BLComponent.Models;
using Moq;

namespace UnitTests;

public class PersonAutoDataAttribute() : AutoDataAttribute(() =>
{
    var fixture = new Fixture();
    fixture.Customize<Person>(c =>
        c.FromFactory(() =>
            new Person(fixture.Create<int>(),
                fixture.Create<string>(),
                fixture.Create<int?>(),
                fixture.Create<string?>(),
                fixture.Create<string?>())));
    return fixture;
});

public class PersonControllerUnitTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock = new();

    [Theory, PersonAutoData]
    public async Task AddPersonTest(Person person)
    {
        _personRepositoryMock.Setup(m => m.AddPersonAsync(person)).ReturnsAsync(1);
        var personManager = new PersonManager(_personRepositoryMock.Object);
        
        var result = await personManager.AddPersonAsync(person);
        
        Assert.Equal(1, result);
        _personRepositoryMock.Verify(m => m.AddPersonAsync(person), Times.Once);
    }

    [Theory, AutoData]
    public async Task RemovePersonTest(int id)
    {
        var personManager = new PersonManager(_personRepositoryMock.Object);
        
        await personManager.RemovePersonAsync(id);
        
        _personRepositoryMock.Verify(m => m.RemovePersonAsync(id), Times.Once);
    }

    [Theory, PersonAutoData]
    public async Task FindPersonTest(Person person)
    {
        _personRepositoryMock.Setup(m => m.FindPersonAsync(person.Id)).ReturnsAsync(person);
        var personManager = new PersonManager(_personRepositoryMock.Object);
        
        var result = await personManager.FindPersonAsync(person.Id);
        
        Assert.Equal(person, result);
        _personRepositoryMock.Verify(m => m.FindPersonAsync(person.Id), Times.Once);
    }

    [Theory, PersonAutoData]
    public async Task GetPersonsTest(Person person1, Person person2, Person person3)
    {
        _personRepositoryMock.Setup(m => m.GetPersonsAsync()).ReturnsAsync([person1, person2, person3]);
        var personManager = new PersonManager(_personRepositoryMock.Object);
        
        var result = await personManager.GetPersonsAsync();
        
        Assert.Equal(3, result.Count);
        _personRepositoryMock.Verify(m => m.GetPersonsAsync(), Times.Once);
    }

    [Theory, PersonAutoData]
    public async Task UpdatePersonTest(Person person)
    {
        var personManager = new PersonManager(_personRepositoryMock.Object);
        
        await personManager.UpdatePersonAsync(person);
        
        _personRepositoryMock.Verify(m => m.UpdatePersonAsync(person), Times.Once);
    }
}