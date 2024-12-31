using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;

public interface IRectanglePlacementStrategy
{
    public void SetCenterPoint(Point center);
    public Point GetNextRectangleLocation(Size rectangleSize);
}