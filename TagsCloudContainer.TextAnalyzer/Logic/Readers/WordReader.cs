using System.Collections.Immutable;
using TagsCloudContainer.TextAnalyzer.Constants;
using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Logic.Readers;

internal sealed class WordReader : IWordReader
{
    public IReadOnlyCollection<string> ReadWords(string text)
    {
        var words = text
            .Split(WordPreprocessorConstants.TextSeparator, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(word => !string.IsNullOrWhiteSpace(word));

        return words.ToImmutableArray();
    }
}