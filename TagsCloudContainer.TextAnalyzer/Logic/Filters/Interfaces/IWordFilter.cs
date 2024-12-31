using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Filters.Interfaces;

internal interface IWordFilter<TDetails>
{
    public Result<TDetails> FilterAvailableByPartSpeech(TDetails wordDetails, WordSettings settings);
}