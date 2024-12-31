using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

public interface IImageSettingsProvider
{
    public ImageSettings GetImageSettings();
    public void SetHeight(int height);
    public void SetWidth(int width);
    public void SetBackgroundColor(Color backgroundColor);
    public void SetWordColor(Color wordColor);
    public void SetFontFamily(FontFamily fontFamily);
}