using System.Text.Json.Serialization;

namespace FbChatClient.Models
{
	internal class Participant
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = "";
	}
}
