using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Logic.Readers;

internal sealed class FileTextReader : IFileTextReader
{
    public string ReadText(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        return ReadText(fileStream);
    }

    private static string ReadText(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}