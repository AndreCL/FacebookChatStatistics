using System.Text.Json.Serialization;

namespace FbChatClient.Models
{
	public class User
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = "";
	}
}
