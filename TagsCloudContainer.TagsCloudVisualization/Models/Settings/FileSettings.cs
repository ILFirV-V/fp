using System.Drawing.Imaging;

namespace TagsCloudContainer.TagsCloudVisualization.Models.Settings;

public record FileSettings
{
    public string InputPath { get; init; } = string.Empty;
    public string OutputPath { get; init; } = string.Empty;
    public string OutputFileName { get; init; } = string.Empty;
    public ImageFormat ImageFormat { get; init; } = ImageFormat.Png;
}