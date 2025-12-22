using System;
using System.Collections.Generic;
using System.Linq;
using ArlenClient.Network.Events;
using Godot;

namespace ArlenClient;

[Flags]
public enum TileState
{
    None,
    Wall
}

public partial class Map : Node
{
    [Export] public PackedScene CharacterScene { get; set; }
    
    public int Width { get; private set; }
    public int Height { get; private set; } 
    public TileState[,] Tiles { get; private set; }

    public void Initialize(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new TileState[width, height];
    }
    
    public List<CharacterController> Characters { get; set; } = [];
    
    public CharacterController SpawnCharacter(SpawnCharacterEvent data)
    {
        var character = CharacterScene.Instantiate<CharacterController>();
        character.Orientation = Orientation.Down;
        character.Speed = 200.0f;   
        character.GridPosition = new Vector2I(data.X, data.Y);
        character.NetworkId = data.Id;
        character.Position = new Vector2(data.X, data.Y) * 32.0f;

        AddChild(character);
        Characters.Add(character);
        
        return character;
    }

    public CharacterController GetCharacter(int id)
    {
        return Characters.FirstOrDefault(c => c.NetworkId == id);
    }

    public CharacterController GetCharacter(Vector2I position)
    {
        return Characters.FirstOrDefault(c => c.GridPosition == position);
    }
    
    public void RemoveCharacter(int id)
    {
        var character = GetCharacter(id);
        if (character == null) 
            return;
        
        Characters.Remove(character);
        character?.QueueFree();
    }
}