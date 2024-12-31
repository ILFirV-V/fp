using TagsCloudContainer.Core;

namespace TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

public interface IFileTextReader
{
    public Result<string> ReadText(string path);
}