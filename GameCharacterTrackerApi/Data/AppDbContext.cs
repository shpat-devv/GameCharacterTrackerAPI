using GameCharacterTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

//EF (Entity Framework) Core is a lightweight version of EF, It's an ORM (Object-Relational Mapping)
//tool that allows developers to interact with databases using .NET objects. In this case GameCharacter will be the entity

namespace GameCharacterTrackerApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<GameCharacter> GameCharacters { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=game_characters.db"); //SQL lite for easy local development
    }
}