using TagsCloudContainer.Core;
using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Filters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Formatters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Preprocessors.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Preprocessors;

internal sealed class TextPreprocessor(
    IWordReader wordReader,
    IAnalyzerFactory analyzerFactory,
    IWordFilter<WordDetails> wordFilter,
    IWordFormatter<WordDetails> wordFormatter)
    : ITextPreprocessor
{
    public Result<IReadOnlyDictionary<string, int>> GetWordFrequencies(string text, WordSettings settings)
    {
        var words = wordReader.ReadWords(text);
        return analyzerFactory.CreateAnalyzer(settings)
            .Then(analyzer => PreprocessWords(analyzer, words, settings));
    }

    private IReadOnlyDictionary<string, int> PreprocessWords(IAnalyzer<WordDetails> wordAnalyzer,
        IReadOnlyCollection<string> words,
        WordSettings settings)
    {
        var wordDetails = words.Aggregate(new List<WordDetails>(), (list, word) =>
        {
            if (wordAnalyzer.TryAnalyzeWord(word, out var result))
            {
                list.Add(result);
            }

            return list;
        });

        return wordDetails
            .Where(d => wordFilter.IsVerify(d, settings))
            .Select(d => wordFormatter.Format(d).FormatedWord)
            .GroupBy(word => word)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}