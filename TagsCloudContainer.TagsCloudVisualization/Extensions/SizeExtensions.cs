using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Extensions;

internal static class SizeExtensions
{
    public static Point Center(this Size size)
    {
        return new Point(size.Width / 2, size.Height / 2);
    }
}