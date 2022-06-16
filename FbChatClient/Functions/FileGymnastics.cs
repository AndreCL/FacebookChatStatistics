using System.IO;
using System.Linq;

namespace FbChatClient.Functions;

public class FileGymnastics
{
    string _inboxFolderLocation;

    public string InboxFolderLocation
    {
        get
        {
            return _inboxFolderLocation;
        }
        set 
        { 
            _inboxFolderLocation = value; 
        }
    }

    public FileGymnastics(string inboxFolderLocation)
    {
        _inboxFolderLocation = inboxFolderLocation;
    }

    /// <summary>
    /// Check that directory exists and contains inbox directory
    /// </summary>
    /// <returns>Number of chats found</returns>
    public int GeneralCheck()
    {
        var result = CheckFolderExists(_inboxFolderLocation);
        if (result)
        {
            result = CheckFolderExists(InboxDirectory());
        }
        if (result)
        {
            return NumberOfChats();
        }

        return 0;
    }

    private string InboxDirectory()
    {
        return _inboxFolderLocation;
    }

    private static bool CheckFolderExists(string directory)
    {
        if (Directory.Exists(directory))
        {
            return true;
        }
        return false;
    }

    private int NumberOfChats()
    {
        var amount = Directory.GetDirectories(InboxDirectory()).Length;

        return amount;
    }

    public string[] ChatDirectories()
    {
        return Directory.GetDirectories(InboxDirectory());
    }

    public string[] GetJsonPathsFromDirectory(string directory)
    {
        return Directory.GetFiles(directory).ToList().Where(x => x.EndsWith(".json")).ToArray();
    }
}
