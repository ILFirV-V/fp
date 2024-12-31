using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Models.Settings;

public record ImageSettings
{
    public Size Size { get; init; } = new(1500, 1500);
    public Color BackgroundColor { get; init; } = Color.White;
    public Color WordColor { get; init; } = Color.Black;
    public FontFamily FontFamily { get; init; } = new("Consolas");
}