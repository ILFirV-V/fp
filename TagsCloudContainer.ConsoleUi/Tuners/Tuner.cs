using CommandLine;
using TagsCloudContainer.ConsoleUi.Tuners.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

namespace TagsCloudContainer.ConsoleUi.Tuners;

public class Tuner(
    IFileSettingsProvider fileSettingsProvider,
    IImageSettingsProvider imageSettingsProvider,
    IWordSettingsProvider wordSettingsProvider)
    : ITuner
{
    public void Tune(string[] arguments)
    {
        Parser.Default.ParseArguments<Options>(arguments)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);
    }

    private void RunOptions(Options options)
    {
        SetFileSettings(options);
        SetWordSettings(options);
        SetImageSettings(options);
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        throw new AggregateException("Ошибка в заданной команде");
    }

    private void SetFileSettings(Options options)
    {
        var currentSettings = fileSettingsProvider.GetPathSettings();
        if (!string.IsNullOrEmpty(options.ImageFormatString) &&
            !Equals(options.ImageFormat, currentSettings.ImageFormat))
        {
            fileSettingsProvider.SetImageFormat(options.ImageFormat);
        }

        if (!string.IsNullOrEmpty(options.InputPath)
            && !Equals(options.InputPath, currentSettings.InputPath))
        {
            fileSettingsProvider.SetInputPath(options.InputPath);
        }

        if (!string.IsNullOrEmpty(options.OutputPath)
            && !Equals(options.OutputPath, currentSettings.OutputPath))
        {
            fileSettingsProvider.SetOutputPath(options.OutputPath);
        }

        if (!string.IsNullOrEmpty(options.OutputFileName) &&
            !Equals(options.OutputFileName, currentSettings.OutputFileName))
        {
            fileSettingsProvider.SetOutputFileName(options.OutputFileName);
        }
    }

    private void SetWordSettings(Options options)
    {
        var currentSettings = wordSettingsProvider.GetWordSettings();
        if (options.ValidSpeechParts.Count != 0
            && !options.ValidSpeechParts.SequenceEqual(currentSettings.ValidSpeechParts))
        {
            wordSettingsProvider.SetValidSpeechParts(options.ValidSpeechParts);
        }
    }

    private void SetImageSettings(Options options)
    {
        var currentSettings = imageSettingsProvider.GetImageSettings();
        if (options.BackgroundColor != default && !options.BackgroundColor.Equals(currentSettings.BackgroundColor))
        {
            imageSettingsProvider.SetBackgroundColor(options.BackgroundColor);
        }

        if (options.WordColor != default && !options.WordColor.Equals(currentSettings.WordColor))
        {
            imageSettingsProvider.SetWordColor(options.WordColor);
        }

        if (options.Height != default && !options.Height.Equals(currentSettings.Size.Height))
        {
            imageSettingsProvider.SetHeight(options.Height);
        }

        if (options.Width != default && !options.Width.Equals(currentSettings.Size.Width))
        {
            imageSettingsProvider.SetWidth(options.Width);
        }

        if (options.FontFamily is not null && !Equals(options.FontFamily, currentSettings.FontFamily))
        {
            imageSettingsProvider.SetFontFamily(options.FontFamily);
        }
    }
}