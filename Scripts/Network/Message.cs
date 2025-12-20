using System.Text.Json;

namespace ArlenClient.Network;

public class Message
{
    private readonly JsonDocument _document;
    
    public string RawData { get; }

    public Opcode Opcode => (Opcode)_document
        .RootElement
        .GetProperty("Opcode").GetInt32();
    
    public Message(string rawData)
    {
        RawData = rawData;
        _document = JsonDocument.Parse(RawData);    
    }

    public T As<T>() where T : class
    {
        return _document.RootElement.Deserialize<T>()!;
    }
}