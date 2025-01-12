using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Filters.Interfaces;

internal interface IWordFilter<in TDetails>
{
    public bool IsVerify(TDetails wordDetails, WordSettings settings);
}