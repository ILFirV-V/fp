namespace TagsCloudContainer.TextAnalyzer.Logic.Formatters.Interfaces;

internal interface IWordFormatter<TWordData>
{
    public TWordData Format(TWordData wordData);
}