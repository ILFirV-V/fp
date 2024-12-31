using System.Collections.Frozen;
using TagsCloudContainer.TextAnalyzer.Models;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Providers;

public class WordSettingsProvider : IWordSettingsProvider
{
    private WordSettings settings = new();

    public WordSettings GetWordSettings() => settings;

    public void SetValidSpeechParts(IEnumerable<string> validSpeechParts)
    {
        settings = settings with { ValidSpeechParts = validSpeechParts.ToFrozenSet() };
    }
}