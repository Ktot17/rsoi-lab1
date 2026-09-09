using BLComponent.Models;

namespace Server.Models;

public record PersonRequest(string Name, int? Age, string? Address, string? Work)
{
    public Person ToPerson() => new Person(0, Name, Age, Address, Work);
    public Person ToPerson(int id) => new Person(id, Name, Age, Address, Work);
}