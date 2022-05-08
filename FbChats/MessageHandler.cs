using System.Text.Json;

namespace FbChats;
public class MessageHandler
{
    private string _inboxFolderLocation;
    private List<Message> messages;

    public FileGymnastics FileGymnastics { get; set; }
    public string MyName { get; set; }
    public int NumberOfChats { get; set; }
    public DateTime First { get; set; }
    public DateTime Last { get; set; }

    public MessageHandler(string inboxFolderLocation)
    {
        _inboxFolderLocation = inboxFolderLocation;

        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }
        catch (Exception ex)
        {

        }

        FileGymnastics = new FileGymnastics(_inboxFolderLocation);
    }

    public void Load()
    {
        NumberOfChats = FileGymnastics.GeneralCheck();

        var messages = GetMessages();

        //User is the one who sent most messages
        var senders = GetTopSenders(false);
        MyName = senders.First().Key;

        First = messages.Min(a => a.MessageDate());
        Last = messages.Max(a => a.MessageDate());
    }

    private List<Message> GetMessages(bool excludeMe = true)
    {
        var directories = FileGymnastics.ChatDirectories();

        messages = new List<Message>();

        foreach (var directory in directories)
        {
            var files = FileGymnastics.GetJsonPathsFromDirectory(directory);

            foreach (var file in files)
            {
                string jsonString = File.ReadAllText(file, System.Text.Encoding.UTF8);

                jsonString = jsonString.Replace("\\u00c3\\u0081", "Á");
                jsonString = jsonString.Replace("\\u00c3\\u00a1", "á");
                jsonString = jsonString.Replace("\\u00c3\\u00a9", "é");
                jsonString = jsonString.Replace("\\u00c3\\u00ad", "í");
                jsonString = jsonString.Replace("\\u00c3\\u00b3", "ó");

                jsonString = jsonString.Replace("\\u00c3\\u00b6", "ö");
                jsonString = jsonString.Replace("\\u00c3\\u00bc", "ü");

                jsonString = jsonString.Replace("\\u00c3\\u00a3", "ã");
                jsonString = jsonString.Replace("\\u00c3\\u00b1", "ñ");

                jsonString = jsonString.Replace("\\u00c3\\u0098", "Ø");
                jsonString = jsonString.Replace("\\u00c3\\u00b8", "ø");
                jsonString = jsonString.Replace("\\u00c3\\u00a6", "æ");
                jsonString = jsonString.Replace("\\u00c3\\u00a5", "å");
                jsonString = jsonString.Replace("\\u00c5\\u0088", "ň");
                jsonString = jsonString.Replace("\\u00c5\\u0099", "ř");
                jsonString = jsonString.Replace("\\u00c4\\u0097", "ė");
                jsonString = jsonString.Replace("\\u00c5\\u00a0", "Š");
                jsonString = jsonString.Replace("\\u00c4\\u008d", "č");
                jsonString = jsonString.Replace("\\u00c3\\u00a7", "ç");


                JsonFile result = JsonSerializer.Deserialize<JsonFile>(jsonString);
                if (result != null)
                {
                    foreach (var message in result.Messages)
                    {
                        if (message.SenderName == "" || message.SenderName == "Other user" || message.SenderName == "Deleted users")
                        {
                            message.SenderName = "Deleted users";
                        }

                        if (!excludeMe || message.SenderName != MyName)
                            messages.Add(message);
                    }
                }
            }
        }

        return messages;
    }

    public Dictionary<string, int> GetTopSenders(bool excludeMe = true)
    {
        Dictionary<string, int> names = new Dictionary<string, int>();
        foreach (var message in messages)
        {
            if (!excludeMe || message.SenderName != MyName)
            {
                if (names.ContainsKey(message.SenderName))
                {
                    names[message.SenderName] += 1;
                }
                else
                {
                    names.Add(message.SenderName, 1);
                }
            }
        }
        return names;
    }

    public Dictionary<string, int> GetTopSenders(int year, bool excludeMe = true)
    {
        Dictionary<string, int> names = new Dictionary<string, int>();
        foreach (var message in messages)
        {
            if (!excludeMe || message.SenderName != MyName)
            {
                if (message.MessageDate().Year == year)
                {
                    if (names.ContainsKey(message.SenderName))
                    {
                        names[message.SenderName] += 1;
                    }
                    else
                    {
                        names.Add(message.SenderName, 1);
                    }
                }
            }
        }
        return names;
    }

}
