namespace TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

internal interface IWordReader
{
    public IReadOnlyCollection<string> ReadWords(string text);
}