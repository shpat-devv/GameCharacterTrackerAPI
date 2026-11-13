using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameCharacterTrackerApi.Models;
using GameCharacterTrackerApi.Data;

namespace GameCharacterTrackerApi.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    //inherit db context
    private readonly AppDbContext _db;
    
    public CharacterController(AppDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GameCharacter>>> GetCharacters()
    {
        return await _db.GameCharacters.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<GameCharacter>> CreateCharacter(GameCharacter character)
    {
        _db.GameCharacters.Add(character);
        await _db.SaveChangesAsync();
    
        return CreatedAtAction(nameof(GetCharacters), new { id = character.Id }, character);
    }

    
    [HttpPut("id:int")]
    public async Task<IActionResult> UpdateCharacter(int id, GameCharacter updatedCharacter)
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