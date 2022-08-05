using System;
using System.Text.Json.Serialization;

namespace FbChatClient.Models;
public class Message
{
    [JsonPropertyName("sender_name")]
    public string SenderName { get; set; } = "";

    [JsonPropertyName("timestamp_ms")]
    public ulong TimeStampMs { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; } = "";

    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    public DateTime MessageDate()
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddMilliseconds(TimeStampMs).ToLocalTime();
        return dateTime;
    }

	#region Call messages
	[JsonPropertyName("call_duration")]
    public int CallDuration { get; set; }

    public TimeSpan Duration()
    {
        return TimeSpan.FromSeconds(CallDuration);
    }
	#endregion
}

