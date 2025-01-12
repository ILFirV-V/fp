using CommandLine;
using TagsCloudContainer.ConsoleUi.Tuners.Interfaces;
using TagsCloudContainer.Core;
using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;
using CommandLineError = CommandLine.Error;
using Error = TagsCloudContainer.Core.Error;

namespace TagsCloudContainer.ConsoleUi.Tuners;

public class Tuner(
    IFileSettingsProvider fileSettingsProvider,
    IImageSettingsProvider imageSettingsProvider,
    IWordSettingsProvider wordSettingsProvider)
    : ITuner
{
    public Result<None> Tune(string[] arguments)
    {
        return Parser.Default.ParseArguments<Options>(arguments)
            .MapResult(RunOptions, HandleParseError);
    }

    private static Result<None> HandleParseError(IEnumerable<CommandLineError> errs)
    {
        var errors = errs.Select(err => err.Tag.ToString()).ToList();
        return new Error($"При чтении опций произошла ошибка. Теги ошибок: {errors}");
    }

    private Result<None> RunOptions(Options options)
    {
        return SetFileSettings(options)
            .Then(_ => SetImageSettings(options))
            .Then(_ => SetWordSettings(options));
    }

    private Result<None> SetFileSettings(Options options)
    {
        var currentSettings = fileSettingsProvider.GetPathSettings();

        return Result.Ok()
            .Then(_ => SetIfChanged(options.InputPath, currentSettings.InputPath,
                fileSettingsProvider.SetInputPath))
            .Then(_ => SetIfChanged(options.OutputPath, currentSettings.OutputPath,
                fileSettingsProvider.SetOutputPath))
            .Then(_ => SetIfChanged(options.OutputFileName, currentSettings.OutputFileName,
                fileSettingsProvider.SetOutputFileName))
            .Then(_ =>
            {
                if (!options.TryGetImageFormat(out var imageFormat))
                {
                    return new Error("Не удалось получить формат изображения");
                }

                return SetIfChanged(imageFormat, currentSettings.ImageFormat,
                    fileSettingsProvider.SetImageFormat);
            });
    }

    private Result<None> SetWordSettings(Options options)
    {
        var currentSettings = wordSettingsProvider.GetWordSettings();
        return Result.Ok()
            .Then(_ => SetIfChanged(options.ValidSpeechParts, currentSettings.ValidSpeechParts,
                wordSettingsProvider.SetValidSpeechParts))
            .Then(_ => SetIfChanged(options.MyStemPath, currentSettings.MyStemPath,
                wordSettingsProvider.SetMyStemPath));
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