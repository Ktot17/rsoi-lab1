using BLComponent.InputPorts;
using BLComponent.Models;
using BLComponent.OutputPorts;

namespace BLComponent;

public class PersonManager(IPersonRepository personRepository) : IPersonManager
{
    public async Task<int> AddPersonAsync(Person person) => await personRepository.AddPersonAsync(person);
    public async Task RemovePersonAsync(int id) => await personRepository.RemovePersonAsync(id);
    public async Task<Person?> FindPersonAsync(int id) => await personRepository.FindPersonAsync(id);
    public async Task<List<Person>> GetPersonsAsync() => await personRepository.GetPersonsAsync();
    public async Task UpdatePersonAsync(Person person) => await personRepository.UpdatePersonAsync(person);
}