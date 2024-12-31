using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Logic.Readers;

internal sealed class FileTextReader : IFileTextReader
{
    public Result<string> ReadText(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new Error($"Файл по указанному пути '{filePath}' не существует.");
        }

        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        return ReadText(fileStream);
    }

    private static Result<string> ReadText(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}