using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Providers;

public class ImageSettingsProvider : IImageSettingsProvider
{
    private ImageSettings imageSettings = new();

    public ImageSettings GetImageSettings() =>
        imageSettings;

    public void SetHeight(int height) =>
        imageSettings = imageSettings with { Size = imageSettings.Size with { Height = height } };

    public void SetWidth(int width) =>
        imageSettings = imageSettings with { Size = imageSettings.Size with { Width = width } };

    public void SetBackgroundColor(Color backgroundColor) =>
        imageSettings = imageSettings with { BackgroundColor = backgroundColor };

    public void SetWordColor(Color wordColor) =>
        imageSettings = imageSettings with { WordColor = wordColor };

    public void SetFontFamily(FontFamily fontFamily) =>
        imageSettings = imageSettings with { FontFamily = fontFamily };
}