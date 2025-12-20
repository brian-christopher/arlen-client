namespace ArlenClient.Network.Commands;

public record CreateCharacterCommand(string Name, int Class, int Race, int Gender) : IPackage
{
    public Opcode Opcode => Opcode.CreateCharacter;
}