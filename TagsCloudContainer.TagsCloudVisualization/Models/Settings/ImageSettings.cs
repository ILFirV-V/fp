using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Models.Settings;

public record ImageSettings
{
    private readonly Size size = new(1500, 1500);

    public Size Size
    {
        get => size;
        init
        {
            if (value.Width <= 0 || value.Height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Width and Height must be positive.");
            }

            size = value;
        }
    }

    public Color BackgroundColor { get; init; } = Color.White;
    public Color WordColor { get; init; } = Color.Black;
    public FontFamily FontFamily { get; init; } = new("Consolas");
}