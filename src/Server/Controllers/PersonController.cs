using BLComponent.Exceptions;
using BLComponent.OutputPorts;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("api/v1/persons/")]
public class PersonController(IPersonManager personManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PersonResponse>>> GetPersons() =>
        Ok((await personManager.GetPersonsAsync()).Select(p => new PersonResponse(p)).ToList());

    [HttpPost]
    public async Task<ActionResult> CreatePerson([FromBody] PersonRequest personRequest)
    {
        var id = await personManager.AddPersonAsync(personRequest.ToPerson());
        return Created($"api/v1/persons/{id}", null);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPerson(int id)
    {
        var person = await personManager.FindPersonAsync(id);
        return person is null ? NotFound(new ErrorResponse($"Person {id} not found")) : Ok(new PersonResponse(person));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePerson(int id)
    {
        try
        {
            await personManager.RemovePersonAsync(id);
        }
        catch (EntityNotFoundException)
        { }
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdatePerson(int id, [FromBody] PersonRequest personRequest)
    {
        var person = personRequest.ToPerson(id);
        try
        {
            person = await personManager.UpdatePersonAsync(person);
        }
        catch (EntityNotFoundException)
        {
            return NotFound(new ErrorResponse($"Person {id} not found"));
        }
        return Ok(new PersonResponse(person));
    }
}