using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Preprocessors.Interfaces;

public interface ITextPreprocessor
{
    public IReadOnlyDictionary<string, int> GetWordFrequencies(string text, WordSettings settings);
}