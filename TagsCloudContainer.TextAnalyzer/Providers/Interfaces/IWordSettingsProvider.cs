using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

public interface IWordSettingsProvider
{
    public WordSettings GetWordSettings();
    public void SetValidSpeechParts(IEnumerable<string> validSpeechParts);
}