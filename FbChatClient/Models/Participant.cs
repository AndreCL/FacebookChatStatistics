using System.Text.Json.Serialization;

namespace FbChatClient.Models;
public class Participant
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = "";
}