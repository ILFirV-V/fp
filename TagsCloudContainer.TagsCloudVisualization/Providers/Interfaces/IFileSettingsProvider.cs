using System.Drawing.Imaging;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

public interface IFileSettingsProvider
{
    public FileSettings GetPathSettings();

    public void SetImageFormat(ImageFormat imageFormat);

    public void SetInputPath(string path);

    public void SetOutputPath(string path);
    public void SetOutputFileName(string fileName);
}