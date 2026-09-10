using BLComponent.Models;

namespace BLComponent.InputPorts;

public interface IPersonRepository
{
    public Task<int> AddPersonAsync(Person person);
    public Task RemovePersonAsync(int id);
    public Task<Person?> FindPersonAsync(int id);
    public Task<List<Person>> GetPersonsAsync();
    public Task<Person> UpdatePersonAsync(Person person);
}