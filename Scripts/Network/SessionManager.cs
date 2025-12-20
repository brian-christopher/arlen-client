using System;
using Godot;

namespace ArlenClient.Network;

public interface ISession
{
    event Action Connected;
    event Action Disconnected;
    event Action<string> MessageReceived;
    
    void SendMessage(string message);
    void ConnectToHost(string host, int port);
    void DisconnectFromHost();
}

public partial class SessionManager : Node, ISession
{
    private readonly WebSocketPeer _socket = new();
    
    public event Action Connected;
    public event Action Disconnected;
    public event Action<string> MessageReceived;
    
    public static ISession Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void ConnectToHost(string host, int port)
    {
        var error = _socket.ConnectToUrl($"ws://{host}:{port}");

        GD.Print(error != Error.Ok
            ? $"Failed to connect to host {host}:{port}"
            : $"Successfully connected to host {host}:{port}");
    }

    public void DisconnectFromHost()
    {
        if (_socket.GetReadyState() == WebSocketPeer.State.Open)
        {
            _socket.Close();
        }
    }

    public void SendMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
            return;
        
        if (_socket.GetReadyState() != WebSocketPeer.State.Open)
            return;
        
        _socket.SendText(message);
    }

    public override void _Process(double _)
    {
        _socket.Poll();
        var state = _socket.GetReadyState();

        if (state == WebSocketPeer.State.Open)
        {
            while (_socket.GetAvailablePacketCount() > 0)
            {
                var packet = _socket.GetPacket();
                var message = packet.GetStringFromUtf8();
                
                MessageReceived?.Invoke(message);
            }
        }
    }
}