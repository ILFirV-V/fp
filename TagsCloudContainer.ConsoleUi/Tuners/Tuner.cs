using CommandLine;
using TagsCloudContainer.ConsoleUi.Tuners.Interfaces;
using TagsCloudContainer.Core;
using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;
using CommandLineError = CommandLine.Error;

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

    private static void HandleParseError(IEnumerable<CommandLineError> errs)
    {
        Console.WriteLine("Ошибка при настройке.");
        Environment.Exit(1);
    }

    private void RunOptions(Options options)
    {
        SetFileSettings(options)
            .Then(_ => SetImageSettings(options))
            .Then(_ => SetWordSettings(options))
            .OnFail(error =>
            {
                Console.WriteLine($"Ошибка при настройке. {error.Message}");
                Environment.Exit(1);
            });
    }

    private Result<None> SetFileSettings(Options options)
    {
        var currentSettings = fileSettingsProvider.GetPathSettings();

        return Result.Ok()
            .Then(_ => SetIfChanged(options.ImageFormat, currentSettings.ImageFormat,
                fileSettingsProvider.SetImageFormat))
            .Then(_ => SetIfChanged(options.InputPath, currentSettings.InputPath, fileSettingsProvider.SetInputPath))
            .Then(_ => SetIfChanged(options.OutputPath, currentSettings.OutputPath, fileSettingsProvider.SetOutputPath))
            .Then(_ => SetIfChanged(options.OutputFileName, currentSettings.OutputFileName,
                fileSettingsProvider.SetOutputFileName));
    }

    private Result<None> SetWordSettings(Options options)
    {
        var currentSettings = wordSettingsProvider.GetWordSettings();
        return Result.Ok()
            .Then(_ => SetIfChanged(options.ValidSpeechParts, currentSettings.ValidSpeechParts,
                wordSettingsProvider.SetValidSpeechParts));
    }

    private Result<None> SetImageSettings(Options options)
    {
        var currentSettings = imageSettingsProvider.GetImageSettings();

        return Result.Ok()
            .Then(_ => SetIfChanged(options.BackgroundColor, currentSettings.BackgroundColor,
                imageSettingsProvider.SetBackgroundColor))
            .Then(_ => SetIfChanged(options.WordColor, currentSettings.WordColor, imageSettingsProvider.SetWordColor))
            .Then(_ => SetIfChanged(options.Height, currentSettings.Size.Height, imageSettingsProvider.SetHeight))
            .Then(_ => SetIfChanged(options.Width, currentSettings.Size.Width, imageSettingsProvider.SetWidth))
            .Then(_ => SetIfChanged(options.FontFamily, currentSettings.FontFamily,
                imageSettingsProvider.SetFontFamily));
    }

    private static Result<None> SetIfChanged<T>(T? newValue, T currentValue, Func<T, Result<None>> setter)
        where T : struct, IEquatable<T>
    {
        if (newValue.HasValue && !newValue.Value.Equals(currentValue))
        {
            return setter(newValue.Value);
        }

        return Result.Ok();
    }

    private static Result<None> SetIfChanged<T>(T? newValue, T currentValue, Func<T, Result<None>> setter)
        where T : class
    {
        if (newValue is not default(T) && !newValue.Equals(currentValue))
        {
            return setter(newValue);
        }

        return Result.Ok();
    }

    private static Result<None> SetIfChanged<T>(ICollection<T> newValues, IEnumerable<T> currentValues,
        Func<IEnumerable<T>, Result<None>> setter)
    {
        if (newValues.Count != 0 && !newValues.SequenceEqual(currentValues))
        {
            return setter(newValues);
        }

        return Result.Ok();
    }
}