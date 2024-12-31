using TagsCloudContainer.Core;

namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;

internal interface IWordAnalyzer<TDetails>
{
    public Result<TDetails> AnalyzeWord(string word);
}