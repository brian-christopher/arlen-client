using Godot;

namespace ArlenClient;

public enum Orientation
{
    Up,
    Down,
    Left,
    Right
}

public static class OrientationExtensions
{
    public static Vector2 ToVector2(this Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Down => Vector2.Down,
            Orientation.Left => Vector2.Left,
            Orientation.Right => Vector2.Right,
            Orientation.Up => Vector2.Up,
            _ => Vector2.Zero
        };
    }
    
    public static Vector2I ToVector2I(this Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Down => Vector2I.Down,
            Orientation.Left => Vector2I.Left,
            Orientation.Right => Vector2I.Right,
            Orientation.Up => Vector2I.Up,
            _ => Vector2I.Zero
        };
    }
}