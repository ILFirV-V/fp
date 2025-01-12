using System.Collections.Frozen;
using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Models;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Providers;

public class WordSettingsProvider : IWordSettingsProvider
{
    private WordSettings settings = new();

    public WordSettings GetWordSettings() => settings;

    public Result<None> SetValidSpeechParts(IEnumerable<string> validSpeechParts)
    {
        var speechParts = validSpeechParts.ToArray();
        if (speechParts.Length == 0)
        {
            return new Error("Должна быть указана хотя бы одна доступная часть речи");
        }

        settings = settings with
        {
            ValidSpeechParts = speechParts.ToFrozenSet()
        };

        return new None();
    }

    public Result<None> SetMyStemPath(string myStemPath)
    {
        if (string.IsNullOrWhiteSpace(myStemPath))
        {
            return new Error("Нужно указать путь до myStem.exe");
        }
        
        settings = settings with
        {
            MyStemPath = myStemPath
        };

        return new None();
    }
}