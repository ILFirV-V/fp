using System.Drawing;
using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

public interface IImageSettingsProvider
{
    public ImageSettings GetImageSettings();
    public Result<None> SetHeight(int height);
    public Result<None> SetWidth(int width);
    public Result<None> SetBackgroundColor(Color backgroundColor);
    public Result<None> SetWordColor(Color wordColor);
    public Result<None> SetFontFamily(FontFamily fontFamily);
}