namespace FbChatClient;
public static class Constants
{
	public static string[] DeletedPseudonyms = new string[] { "", "Other user", "Deleted users", "Facebook User" };

	public static string DeletedValue = "Deleted users";

	//The below ones determines which type of messages are included in the graphs
	public const bool ExcludeSubscribe = false;
	public const bool ExcludeUnsubscribe = false;
	public const bool ExcludeCall = false;
}
