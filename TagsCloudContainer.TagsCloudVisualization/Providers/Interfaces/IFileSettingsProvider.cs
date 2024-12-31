using System.Drawing.Imaging;
using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

public interface IFileSettingsProvider
{
    public FileSettings GetPathSettings();

    public Result<None> SetImageFormat(ImageFormat imageFormat);

    public Result<None> SetInputPath(string path);

    public Result<None> SetOutputPath(string path);
    public Result<None> SetOutputFileName(string fileName);
}