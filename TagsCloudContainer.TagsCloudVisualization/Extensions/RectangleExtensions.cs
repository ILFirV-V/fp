using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Extensions;

internal static class RectangleExtensions
{
    public static Point Center(this Rectangle rectangle)
    {
        return new Point(rectangle.Left + rectangle.Width / 2,
            rectangle.Top + rectangle.Height / 2);
    }
}