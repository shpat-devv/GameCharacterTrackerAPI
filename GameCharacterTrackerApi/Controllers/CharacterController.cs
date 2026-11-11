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

    /*
    [HttpPut("id:int")]
    public async Task<IActionResult> UpdateProduct(int id, GameCharacter character)
    {
        var character = aw
    }
    
    
    [HttpDelete("id:int")]
    */
    
    
}