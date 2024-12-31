using System.Drawing.Imaging;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Providers;

public class FileSettingsProvider : IFileSettingsProvider
{
    private FileSettings fileSettings = new();

    public FileSettings GetPathSettings() =>
        fileSettings;

    public void SetImageFormat(ImageFormat imageFormat) =>
        fileSettings = fileSettings with { ImageFormat = imageFormat };

    public void SetInputPath(string path) =>
        fileSettings = fileSettings with { InputPath = path };

    public void SetOutputPath(string path) =>
        fileSettings = fileSettings with { OutputPath = path };

    public void SetOutputFileName(string fileName) =>
        fileSettings = fileSettings with { OutputFileName = fileName };
}