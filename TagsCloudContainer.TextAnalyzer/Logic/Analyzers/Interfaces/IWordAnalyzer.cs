namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;

internal interface IWordAnalyzer<out TDetails>
{
    public TDetails? AnalyzeWordOrNull(string word);
}