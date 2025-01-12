using System.Drawing.Imaging;
using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Providers;

public class FileSettingsProvider : IFileSettingsProvider
{
    private FileSettings fileSettings = new();

    public FileSettings GetPathSettings() => fileSettings;

    public Result<None> SetImageFormat(ImageFormat imageFormat)
    {
        fileSettings = fileSettings with
        {
            ImageFormat = imageFormat
        };

        return Result.Ok();
    }

    public Result<None> SetInputPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return new Error("Нужно указать путь до файла с входными данными.");
        }

        fileSettings = fileSettings with
        {
            InputPath = path
        };

        return Result.Ok();
    }

    public Result<None> SetOutputPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return new Error("Нужно указать путь до папки куда будет сохранен итоговый файл.");
        }

        fileSettings = fileSettings with
        {
            OutputPath = path
        };

        return Result.Ok();
    }

    public Result<None> SetOutputFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return new Error("Имя итогового файла не может быть пустым");
        }

        fileSettings = fileSettings with
        {
            OutputFileName = fileName
        };

        return Result.Ok();
    }
}