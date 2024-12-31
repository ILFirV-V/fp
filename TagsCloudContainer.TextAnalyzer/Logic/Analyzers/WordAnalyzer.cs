using MyStemWrapper;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers;

internal sealed class WordAnalyzer(MyStem myStem) : IWordAnalyzer<WordDetails>
{
    public WordDetails? AnalyzeWordOrNull(string word)
    {
        var analyzedWordInfo = myStem.Analysis(word);
        if (string.IsNullOrWhiteSpace(analyzedWordInfo))
        {
            return null;
        }

        var parts = analyzedWordInfo.Split('=');
        if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            return null;
        }

        var speechParts = parts[1].Split(',');
        if (speechParts.Length == 0 || string.IsNullOrWhiteSpace(speechParts[0]))
        {
            return null;
        }

        return new WordDetails(word, parts[0].Trim(), speechParts[0].Trim());
    }
}