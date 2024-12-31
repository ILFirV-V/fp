using System.Drawing;
using System.Drawing.Text;
using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Providers;

public class ImageSettingsProvider : IImageSettingsProvider
{
    private ImageSettings imageSettings = new();

    public ImageSettings GetImageSettings() => imageSettings;

    public Result<None> SetHeight(int height)
    {
        if (height <= 0)
        {
            return new Error("Высота должна быть больше 0");
        }

        imageSettings = imageSettings with
        {
            Size = imageSettings.Size with
            {
                Height = height
            }
        };

        return Result.Ok();
    }

    public Result<None> SetWidth(int width)
    {
        if (width <= 0)
        {
            return new Error("Ширина должна быть больше 0");
        }

        imageSettings = imageSettings with
        {
            Size = imageSettings.Size with
            {
                Width = width
            }
        };

        return Result.Ok();
    }

    public Result<None> SetBackgroundColor(Color backgroundColor)
    {
        imageSettings = imageSettings with
        {
            BackgroundColor = backgroundColor
        };

        return Result.Ok();
    }

    public Result<None> SetWordColor(Color wordColor)
    {
        imageSettings = imageSettings with
        {
            WordColor = wordColor
        };

        return Result.Ok();
    }

    public Result<None> SetFontFamily(FontFamily fontFamily)
    {
        var installedFonts = new InstalledFontCollection();
        var fontFamilies = installedFonts.Families;

        if (!fontFamilies.Any(f => f.Name.Equals(fontFamily.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return new Error($"Шрифт '{fontFamily.Name}' не установлен в системе.");
        }

        imageSettings = imageSettings with
        {
            FontFamily = fontFamily
        };

        return Result.Ok();
    }
}