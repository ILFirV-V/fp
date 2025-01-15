using TagsCloudContainer.ConsoleUi.Runner.Interfaces;
using TagsCloudContainer.ConsoleUi.Tuners.Interfaces;
using TagsCloudContainer.Core;
using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Preprocessors.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

namespace TagsCloudContainer.ConsoleUi.Runner;

public class TagsCloudContainerUi(
    ITuner tuner,
    IFileTextReader fileReader,
    ITextPreprocessor textPreprocessor,
    IWordsCloudVisualizer wordsCloudVisualizer,
    IFileSettingsProvider fileSettingsProvider,
    IImageSettingsProvider imageSettingsProvider,
    IWordSettingsProvider wordSettingsProvider)
    : ITagsCloudContainerUi
{
    public Result<None> Run(string[] args)
    {
        return tuner.Tune(args).Then(_ => GenerateFile());
    }

    private Result<None> GenerateFile()
    {
        var pathSettings = fileSettingsProvider.GetPathSettings();
        var imageSettings = imageSettingsProvider.GetImageSettings();
        var wordSettings = wordSettingsProvider.GetWordSettings();
        return fileReader.ReadText(pathSettings.InputPath)
            .Then(text => textPreprocessor.GetWordFrequencies(text, wordSettings))
            .Then(analyzeWords => wordsCloudVisualizer.CreateImage(imageSettings, analyzeWords))
            .Then(image =>
            {
                using (image)
                {
                    return wordsCloudVisualizer.SaveImage(image, pathSettings);
                }
            });
    }
}