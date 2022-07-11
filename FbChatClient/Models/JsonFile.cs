using System.Text.Json.Serialization;

namespace FbChatClient.Models;

internal class JsonFile
{
    [JsonPropertyName("messages")]
    public Message[] Messages { get; set; } = System.Array.Empty<Message>();

    [JsonPropertyName("participants")]
	public Participant[] Participants { get; set; } = System.Array.Empty<Participant>();

}
