using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Preprocessors.Interfaces;

public interface ITextPreprocessor
{
    public Result<IReadOnlyDictionary<string, int>> GetWordFrequencies(string text, WordSettings settings);
}