using System.Drawing;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;

public interface IRectanglePlacementStrategy
{
    public Result<None> SetCenterPoint(Point center);
    public Result<Point> GetNextRectangleLocation(Size rectangleSize);
}