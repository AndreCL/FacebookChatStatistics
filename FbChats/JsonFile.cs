using System.Text.Json.Serialization;

namespace FbChats;

internal class JsonFile
{
    [JsonPropertyName("messages")]
    public Message[] Messages { get; set; }
}
