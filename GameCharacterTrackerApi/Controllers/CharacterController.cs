using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameCharacterTrackerApi.Models;
using GameCharacterTrackerApi.Dtos;
using GameCharacterTrackerApi.Data;

namespace GameCharacterTrackerApi.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    //inherit db context
    private readonly AppDbContext _db;
    private int _id;
    public CharacterController(AppDbContext db)
    {
        _db = db;
        _id = _db.GameCharacters.Max(c => c.Id) ?? 0;
        Console.WriteLine(_id);
    }
    
    private int GetNextId()
    {
        return _id++;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GameCharacter>>> GetCharacters()
    {
        return await _db.GameCharacters.ToListAsync();
    }
    
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameCharacter>> GetCharacter(int id)
    {
        var character = await _db.GameCharacters.FindAsync(id);
        
        if (character == null)
        {
            return NotFound();
        }

        return character;
    }
    
    [HttpPost]
    public async Task<ActionResult<GameCharacter>> CreateCharacter(PostCharacterDto character)
    {
        var newCharacter = new GameCharacter
        {
            Id = GetNextId(),
            Name = character.Name,
            Game = character.Game,
            Role = character.Role,
            Power = character.Power,
            Rating = character.Rating
        };

        _db.GameCharacters.Add(newCharacter);
        await _db.SaveChangesAsync();
    
        return CreatedAtAction(nameof(GetCharacters), new { id = newCharacter.Id }, character);
    }

    
    [HttpPut("id:int")]
    public async Task<IActionResult> UpdateCharacter(int id, PostCharacterDto updatedCharacter)
    {
        var character = await _db.GameCharacters.FindAsync(id);

        if (character is null)
        {
            return NotFound();
        }
        
        character.Name = updatedCharacter.Name;
        character.Game = updatedCharacter.Game;
        character.Role = updatedCharacter.Role;
        character.Power = updatedCharacter.Power;
        character.Rating = updatedCharacter.Rating;

        await _db.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("id:int")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var character = await _db.GameCharacters.FindAsync(id);
        
        if (character is null)
        {
            return NotFound();
        }

        _db.GameCharacters.Remove(character);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}