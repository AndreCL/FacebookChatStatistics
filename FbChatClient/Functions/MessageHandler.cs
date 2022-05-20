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

                jsonString = TextProcessing.FixFBEncodingIssues(jsonString);

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
