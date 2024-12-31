using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Strategies;

public class SpiralPlacementStrategy : IRectanglePlacementStrategy
{
    private int layer;
    private double angle;
    private Point center;
    private const double FullCircleTurn = Math.PI * 2;
    private const int DistanceLayersDifference = 1;
    private const double BetweenAngleDifference = Math.PI / 36;

    public void SetCenterPoint(Point center)
    {
        this.center = center;
    }

    public Point GetNextRectangleLocation(Size rectangleSize)
    {
        if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
        {
            throw new ArgumentException("Размер ширины и высоты должен быть больше 0.");
        }

        var shiftFromCenter = -1 * rectangleSize / 2;
        if (layer == 0)
        {
            layer = CalculateNextLayer(layer);
            return center + shiftFromCenter;
        }

        var x = center.X + layer * DistanceLayersDifference * Math.Cos(angle);
        var y = center.Y + layer * DistanceLayersDifference * Math.Sin(angle);

        angle = CalculateNextAngle(angle);
        if (ShouldUpdateLayer(angle))
        {
            layer = CalculateNextLayer(layer);
            angle %= FullCircleTurn;
        }

        return new Point((int) x, (int) y) + shiftFromCenter;
    }

    private static double CalculateNextAngle(double oldAngle)
    {
        return oldAngle + BetweenAngleDifference;
    }

    private static bool ShouldUpdateLayer(double oldAngle)
    {
        return oldAngle > FullCircleTurn;
    }

    private static int CalculateNextLayer(int oldLayer)
    {
        return oldLayer + 1;
    }
}