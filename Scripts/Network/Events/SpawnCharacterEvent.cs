namespace ArlenClient.Network.Events;

public record SpawnCharacterEvent(
    int Id,
    string Name,
    int X,
    int Y,
    int Body,
    int Head,
    int Helmet,
    int Weapon,
    int Shield) : IPackage
{
    public Opcode Opcode => Opcode.SpawnCharacter;
}