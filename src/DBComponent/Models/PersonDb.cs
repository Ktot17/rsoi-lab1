using System.ComponentModel.DataAnnotations.Schema;

namespace DBComponent.Models;

[Table("people")]
public class PersonDb(int id, string name, int? age, string? address, string? work)
{
    public PersonDb(BLComponent.Models.Person person) : this(person.Id, person.Name, person.Age, person.Address,
        person.Work) {}

    [Column("id")]
    public int Id { get; init; } = id;

    [Column("name")] 
    public string Name { get; set; } = name;

    [Column("age")] 
    public int? Age { get; set; } = age;
    
    [Column("address")]
    public string? Address { get; set; } = address;
    
    [Column("work")]
    public string? Work { get; set; } = work;
    
    public BLComponent.Models.Person ToBlModel() => new BLComponent.Models.Person(Id, Name, Age, Address, Work);
}