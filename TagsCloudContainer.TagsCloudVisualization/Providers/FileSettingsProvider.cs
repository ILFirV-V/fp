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
        if (!File.Exists(path))
        {
            return new Error($"Файл по указанному пути '{path}' не существует.");
        }

        fileSettings = fileSettings with
        {
            InputPath = path
        };

        return Result.Ok();
    }

    public Result<None> SetOutputPath(string path)
    {
        if (!Directory.Exists(path))
        {
            return new Error($"Папки по указанному пути '{path}' не существует.");
        }

        fileSettings = fileSettings with
        {
            OutputPath = path
        };

        return Result.Ok();
    }

    public Result<None> SetOutputFileName(string fileName)
    {
        var expectedFilePath = Path.Combine(fileSettings.OutputPath, fileName);
        if (File.Exists(expectedFilePath))
        {
            return new Error($"Файл с именем '{fileName}' в папке '{fileSettings.OutputPath}' уже существует.");
        }

        fileSettings = fileSettings with
        {
            OutputFileName = fileName
        };

        return Result.Ok();
    }
}