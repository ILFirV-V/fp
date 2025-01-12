namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;

public interface IAnalyzer<TDetails>
{
    public bool TryAnalyzeWord(string word, out TDetails wordDetails);
}