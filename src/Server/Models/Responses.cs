using BLComponent.Models;

namespace Server.Models;

public record PersonResponse(int Id, string Name, int? Age, string? Address, string? Work)
{
    public PersonResponse(Person person) : this(person.Id, person.Name, person.Age, person.Address, person.Work) {}
}

public record ErrorResponse(string Message);

public record ValidationErrorResponse(string Message, Dictionary<string, string> Errors);