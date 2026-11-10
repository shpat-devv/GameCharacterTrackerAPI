namespace GameCharacterTrackerApi.Models;

//TODO: Add string length limit
public class GameCharacter
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Game { get; set; }
    public required string Role { get; set; }
    public required int Power { get; set; }
    public required int Rating { get; set; }
}