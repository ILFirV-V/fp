using MyStemWrapper;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers;

public class WordAnalyzer(MyStem myStem) : IAnalyzer<WordDetails>
{
    public bool TryAnalyzeWord(string word, out WordDetails wordDetails)
    {
        wordDetails = new WordDetails(word, "unknown", "unknown");
        string analyzedWordInfo;
        try
        {
            analyzedWordInfo = myStem.Analysis(word);
        }
        catch
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(analyzedWordInfo))
        {
            return false;
        }

        var parts = analyzedWordInfo.Split('=');
        if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            return false;
        }

        var speechParts = parts[1].Split(',');
        if (speechParts.Length == 0 || string.IsNullOrWhiteSpace(speechParts[0]))
        {
            return false;
        }

        wordDetails = new WordDetails(word, parts[0].Trim(), speechParts[0].Trim());
        return true;
    }
}