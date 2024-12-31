using System.Collections.Frozen;
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
    public IReadOnlyDictionary<string, int> GetWordFrequencies(string text, WordSettings settings)
    {
        var wordFrequencies = new Dictionary<string, int>();
        var words = wordReader.ReadWords(text);
        foreach (var word in words)
        {
            if (!TryPreprocessWord(word, settings, out var processedWord))
            {
                continue;
            }

            wordFrequencies.TryAdd(processedWord, 0);
            wordFrequencies[processedWord]++;
        }

        return wordFrequencies.ToFrozenDictionary();
    }

    private bool TryPreprocessWord(string word, WordSettings settings, out string processedWord)
    {
        processedWord = string.Empty;
        var wordDetails = wordAnalyzer.AnalyzeWordOrNull(word);
        if (wordDetails is null || !wordFilter.IsVerify(wordDetails, settings))
        {
            return false;
        }

        var formatedWordDetails = wordFormatter.Format(wordDetails);
        processedWord = formatedWordDetails.FormatedWord;
        return true;
    }
}