using TagsCloudContainer.TextAnalyzer.Logic.Filters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Filters;

internal sealed class WordFilter : IWordFilter<WordDetails>
{
    public bool IsVerify(WordDetails wordDetails, WordSettings settings)
    {
        return settings.ValidSpeechParts.Contains(wordDetails.SpeechPart);
    }
}