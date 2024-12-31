using TagsCloudContainer.Core;

namespace TagsCloudContainer.TextAnalyzer.Logic.Formatters.Interfaces;

internal interface IWordFormatter<TWordData>
{
    public Result<TWordData> Format(TWordData wordData);
}