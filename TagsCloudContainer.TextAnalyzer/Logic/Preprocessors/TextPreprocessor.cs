using System.Collections.Frozen;
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
    IWordAnalyzer<WordDetails> wordAnalyzer,
    IWordFilter<WordDetails> wordFilter,
    IWordFormatter<WordDetails> wordFormatter)
    : ITextPreprocessor
{
    public Result<IReadOnlyDictionary<string, int>> GetWordFrequencies(string text, WordSettings settings)
    {
        return wordReader
            .ReadWords(text)
            .Then(words => PreprocessWords(words, settings));
    }

    private Result<IReadOnlyDictionary<string, int>> PreprocessWords(IReadOnlyCollection<string> words,
        WordSettings settings)
    {
        return words
            .Select(word => wordAnalyzer.AnalyzeWord(word)
                .Then(details => wordFilter.FilterAvailableByPartSpeech(details, settings))
                .Then(wordFormatter.Format)
                .Then(details => details.FormatedWord))
            .Where(r => r.IsSuccess)
            .Select(res => res.GetValueOrThrow())
            .Aggregate(new Dictionary<string, int>(), (freq, word) =>
            {
                freq.TryAdd(word, 0);
                freq[word]++;
                return freq;
            })
            .ToFrozenDictionary();
    }
}