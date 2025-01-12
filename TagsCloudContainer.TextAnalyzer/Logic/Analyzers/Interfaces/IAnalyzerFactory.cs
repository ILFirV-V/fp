using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;

public interface IAnalyzerFactory
{
    public Result<IAnalyzer<WordDetails>> CreateAnalyzer(WordSettings settings);
}