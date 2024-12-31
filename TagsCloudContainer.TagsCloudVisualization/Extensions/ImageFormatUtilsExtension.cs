using System.Drawing.Imaging;

namespace TagsCloudContainer.TagsCloudVisualization.Extensions;

internal static class ImageFormatUtilsExtension
{
    public static string FileExtensionFromToString(this ImageFormat format)
    {
        return "." + format.ToString().ToLower();
    }
}