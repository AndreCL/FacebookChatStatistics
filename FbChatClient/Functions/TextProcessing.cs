namespace FbChatClient.Functions;
public static class TextProcessing
{
    /// <summary>
    /// Due to issues with FB message encoding, this monstruosity of a code is necessary for cleanup
    /// https://stackoverflow.com/questions/50008296/facebook-json-badly-encoded
    /// This list is definitely not complete.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string FixFBEncodingIssues(string input)
    {
        input = input.Replace("\\u00c3\\u0081", "Á");
        input = input.Replace("\\u00c3\\u00a1", "á");
        input = input.Replace("\\u00c3\\u00a9", "é");
        input = input.Replace("\\u00c3\\u00ad", "í");
        input = input.Replace("\\u00c3\\u00b3", "ó");

        input = input.Replace("\\u00c3\\u00b6", "ö");
        input = input.Replace("\\u00c3\\u00bc", "ü");

        input = input.Replace("\\u00c3\\u00a3", "ã");
        input = input.Replace("\\u00c3\\u00b1", "ñ");

        input = input.Replace("\\u00c3\\u0098", "Ø");
        input = input.Replace("\\u00c3\\u00b8", "ø");
        input = input.Replace("\\u00c3\\u00a6", "æ");
        input = input.Replace("\\u00c3\\u00a5", "å");
        input = input.Replace("\\u00c5\\u0088", "ň");
        input = input.Replace("\\u00c5\\u0099", "ř");
        input = input.Replace("\\u00c4\\u0097", "ė");
        input = input.Replace("\\u00c5\\u00a0", "Š");
        input = input.Replace("\\u00c4\\u008d", "č");
        input = input.Replace("\\u00c3\\u00a7", "ç");

        return input;
    }
}
