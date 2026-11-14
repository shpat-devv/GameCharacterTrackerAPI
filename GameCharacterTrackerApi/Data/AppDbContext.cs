using GameCharacterTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

//EF (Entity Framework) Core is a lightweight version of EF, It's an ORM (Object-Relational Mapping)
//tool that allows developers to interact with databases using .NET objects. In this case GameCharacter will be the entity

//TODO: Primary Constructor fix
namespace GameCharacterTrackerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<GameCharacter> GameCharacters { get; set; } // DbSet is a collection of entities of type GameCharacter
    
    public int GetNextId()
    {
        return GameCharacters.Max(c => c.Id) + 1;
    }
}