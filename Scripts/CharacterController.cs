using Godot;

namespace ArlenClient;

public partial class CharacterController : Node2D
{
    private Vector2 _targetPosition;
    
    public Vector2I GridPosition {get; set;}
    public bool IsWalking {get; set;}
    public Orientation Orientation { get; set; }
    public float Speed { get; set; } = 200.0f;
    public int NetworkId { get; set; }
    
    public void MoveTo(Orientation orientation)
    {
        if (IsWalking)
        {
            Stop();
        }
        
        _targetPosition = Position + (Orientation.ToVector2() * 128.0f);
        IsWalking = true;
        Orientation = orientation;
    }

    public void Stop()
    {
        
    }

    public override void _Process(double delta)
    {
        ProcessMovement((float)delta);
    }

    private void ProcessMovement(float delta)
    {
        if (IsWalking)
        {
            Position = Position.MoveToward(_targetPosition, Speed * delta);

            if (Position == _targetPosition)
            {
                IsWalking = false;
            }
        }
    }
}