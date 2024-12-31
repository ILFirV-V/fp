using MyStemWrapper;
using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers;

internal sealed class WordAnalyzer(MyStem myStem) : IWordAnalyzer<WordDetails>
{
    public Result<WordDetails> AnalyzeWord(string word)
    {
        string analyzedWordInfo;
        try
        {
            analyzedWordInfo = myStem.Analysis(word);
        }
        catch (Exception e)
        {
            return new Error($"Во время анализа слова произошла ошибка. {e.Message}");
        }

        if (string.IsNullOrWhiteSpace(analyzedWordInfo))
        {
            return new Error("Во время анализа слова произошла ошибка.");
        }

        var parts = analyzedWordInfo.Split('=');
        if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            return new Error("Данных о слове после анализа слишком мало.");
        }

        var speechParts = parts[1].Split(',');
        if (speechParts.Length == 0 || string.IsNullOrWhiteSpace(speechParts[0]))
        {
            return new Error("Данных о слове после анализа слишком мало. Не смогли узнать часть речи");
        }

        return new WordDetails(word, parts[0].Trim(), speechParts[0].Trim());
    }
}