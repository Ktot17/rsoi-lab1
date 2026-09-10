using BLComponent.Models;

namespace BLComponent.OutputPorts;

public interface IPersonManager
{
    public Task<int> AddPersonAsync(Person person);
    public Task RemovePersonAsync(int id);
    public Task<Person?> FindPersonAsync(int id);
    public Task<List<Person>> GetPersonsAsync();
    public Task UpdatePersonAsync(Person person);
}