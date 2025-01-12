using System.Drawing;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;

public interface IRectanglePlacementStrategy
{
    public void SetCenterPoint(Point setCenter);
    public Result<Point> GetNextRectangleLocation(Size rectangleSize);
}