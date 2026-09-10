using BLComponent.Exceptions;
using BLComponent.InputPorts;
using BLComponent.Models;
using DBComponent.Models;
using Microsoft.EntityFrameworkCore;

namespace DBComponent.Postgres;

public class PostgresPersonRepository(PostgresDbContext context) : IPersonRepository
{
    public async Task<int> AddPersonAsync(Person person)
    {
        var added = await context.AddAsync(new PersonDb(person));
        await context.SaveChangesAsync();
        return added.Entity.Id;
    }

    public async Task RemovePersonAsync(int id)
    {
        var person = await context.FindAsync<PersonDb>(id) ?? throw new EntityNotFoundException(typeof(PersonDb));
        context.Remove(person);
        await context.SaveChangesAsync();
    }

    public async Task<Person?> FindPersonAsync(int id) => 
        (await context.FindAsync<PersonDb>(id))?.ToBlModel();

    public async Task<List<Person>> GetPersonsAsync() => 
        await context.Set<PersonDb>().Select(p => p.ToBlModel()).ToListAsync();

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var existing = await context.FindAsync<PersonDb>(person.Id) ?? throw new EntityNotFoundException(typeof(PersonDb));
        var newEntry = new PersonDb(
            person.Id,
            person.Name,
            person.Age ?? existing.Age,
            person.Address ?? existing.Address,
            person.Work ?? existing.Work);
        context.Entry(existing).CurrentValues.SetValues(newEntry);
        await context.SaveChangesAsync();
        return newEntry.ToBlModel();
    }
}