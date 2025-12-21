using ArlenClient.Network;
using ArlenClient.Network.Events;
using Godot;

namespace ArlenClient;

public partial class Main : Node
{
	private CharacterController _characterController;
	
	
	public override async void _Ready()
	{
		_characterController = GetNode<CharacterController>("Character");
		
		SessionManager.Instance.MessageReceived += OnMessageReceived;
		SessionManager.Instance.ConnectToHost("127.0.0.1", 7666);

		await ToSignal(GetTree().CreateTimer(1), SceneTreeTimer.SignalName.Timeout);
		SessionManager.Instance.SendMessage("{Opcode: 4}");
		
		await ToSignal(GetTree().CreateTimer(1.5), SceneTreeTimer.SignalName.Timeout);
		_characterController.MoveTo(Orientation.Down);
		
		await ToSignal(GetTree().CreateTimer(1.5), SceneTreeTimer.SignalName.Timeout);
		//_characterController.MoveTo(Orientation.Right);
		
		await ToSignal(GetTree().CreateTimer(1.5), SceneTreeTimer.SignalName.Timeout);
		//_characterController.MoveTo(Orientation.Right);

	}

	private void OnMessageReceived(string data)
	{
		var message = new Message(data);

		switch (message.Opcode)
		{
			case Opcode.ChangeMap:
				HandleChangeMap(message.As<ChangeMapEvent>());
				break;
			case Opcode.SpawnCharacter:
				HandleSpawnCharacter(message.As<SpawnCharacterEvent>());
				break;
		}
	}

	private void HandleSpawnCharacter(SpawnCharacterEvent data)
	{
		
	}

	private void HandleChangeMap(ChangeMapEvent data)
	{
		
	}

	public override void _Process(double delta)
	{
	}
}