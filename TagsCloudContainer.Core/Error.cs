namespace TagsCloudContainer.Core;

public class Error(string message)
{
    public string Message { get; } = message;
}