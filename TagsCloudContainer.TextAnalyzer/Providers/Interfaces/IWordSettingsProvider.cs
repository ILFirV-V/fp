using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

public interface IWordSettingsProvider
{
    public WordSettings GetWordSettings();
    public Result<None> SetValidSpeechParts(IEnumerable<string> validSpeechParts);
    public Result<None> SetMyStemPath(string myStemPath);
}