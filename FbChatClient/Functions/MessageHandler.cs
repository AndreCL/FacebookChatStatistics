using FbChatClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FbChatClient.Functions;
public class MessageHandler
{
    private string _inboxFolderLocation;
    private List<Message> messages = new();

    private List<JsonFile> rawFiles = new();

    public FileGymnastics FileGymnastics { get; set; }
    public string MyName { get; set; } = "";
    public int NumberOfChats { get; set; }
    public DateTime First { get; set; }
    public DateTime Last { get; set; }

    public MessageHandler(string inboxFolderLocation)
    {
        _inboxFolderLocation = inboxFolderLocation;

        FileGymnastics = new FileGymnastics(_inboxFolderLocation);
    }

    public void Load()
    {
        NumberOfChats = FileGymnastics.GeneralCheck();

        var messages = GetMessages();

        //User is the one who sent most messages
        var senders = GetTopSenders(false);

        if(senders.Count == 0) return; //return if none found

        MyName = senders.OrderByDescending(x => x.Value).First().Key;

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

                jsonString = TextProcessing.FixFBEncodingIssues(jsonString);

                var result = JsonSerializer.Deserialize<JsonFile>(jsonString);
                if (result != null)
                {
                    foreach(var participant in result.Participants)
					{
                        if (participant.Name == "" || participant.Name == "Other user" || participant.Name == "Deleted users" || participant.Name == "Facebook User")
                        {
                            participant.Name = "Deleted users";
                        }
                    }

                    foreach (var message in result.Messages)
                    {
                        if (message.SenderName == "" || message.SenderName == "Other user" || message.SenderName == "Deleted users" || message.SenderName == "Facebook User")
                        {
                            message.SenderName = "Deleted users";
                        }

                        if (!excludeMe || message.SenderName != MyName)
                            messages.Add(message);
                    }

                    rawFiles.Add(result);
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

    public Dictionary<string, int> GetTopSenders(int year, int month, bool excludeMe = true)
    {
        Dictionary<string, int> names = new Dictionary<string, int>();
        foreach (var message in messages)
        {
            if (!excludeMe || message.SenderName != MyName)
            {
                if (message.MessageDate().Year == year && message.MessageDate().Month == month)
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

    public int GetNumberOfSentForName(string Name)
	{
        //number of chats with this participant
        var chats = rawFiles.Where(x => x.Participants.Any(y => y.Name == Name));

		if (chats.Any())
		{
            var messages = chats.SelectMany(x => x.Messages);

			if (messages.Any())
			{
                var result = messages.Count(x => x.SenderName.Equals(MyName));

                return result;
			}
		}

        return 0;

	}

    public int GetNumberOfSentForName(string Name, int year)
    {
        //number of chats with this participant
        var chats = rawFiles.Where(x => x.Participants.Any(y => y.Name == Name));

        if (chats.Any())
        {
            var messages = chats.SelectMany(x => x.Messages);

            if (messages.Any())
            {
                var result = messages.Count(x => x.SenderName.Equals(MyName) && x.MessageDate().Year == year);

                return result;
            }
        }

        return 0;
    }
}
