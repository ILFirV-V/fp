using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Logic.Filters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Filters;

internal sealed class WordFilter : IWordFilter<WordDetails>
{
    public Result<WordDetails> FilterAvailableByPartSpeech(WordDetails wordDetails, WordSettings settings)
    {
        return settings.ValidSpeechParts.Contains(wordDetails.SpeechPart)
            ? wordDetails
            : new Error("Слово не проходит по типу доступных частй речи.");
    }
}