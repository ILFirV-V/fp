using MyStemWrapper;
using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Logic.Analyzers;

internal sealed class AnalyzerFactory : IAnalyzerFactory
{
    public Result<IAnalyzer<WordDetails>> CreateAnalyzer(WordSettings settings)
    {
        if (!File.Exists(settings.MyStemPath))
        {
            return new Error($"Файл MyStem не найден по пути: {settings.MyStemPath}");
        }

        var myStem = new MyStem
        {
            PathToMyStem = settings.MyStemPath,
            Parameters = "-nli"
        };

        return new WordAnalyzer(myStem);
    }
}