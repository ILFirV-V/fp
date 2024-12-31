using TagsCloudContainer.Core;

namespace TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

internal interface IWordReader
{
    public Result<IReadOnlyCollection<string>> ReadWords(string text);
}