using TagsCloudContainer.TextAnalyzer.Logic.Formatters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Formatters;

internal sealed class WordCaseFormatter : IWordFormatter<WordDetails>
{
    public WordDetails Format(WordDetails wordDetails)
    {
        var newWordDetails = wordDetails with
        {
            FormatedWord = wordDetails.FormatedWord.ToLower()
        };
        return newWordDetails;
    }
}