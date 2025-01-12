using System.Drawing;
using TagsCloudContainer.Core;
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

    public void SetCenterPoint(Point setCenter)
    {
        center = setCenter;
    }

    public Result<Point> GetNextRectangleLocation(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
        {
            return new Error("Размер ширины и высоты должен быть больше 0.");
        }

        var shiftFromCenter = -1 * rectangleSize / 2;
        if (layer == 0)
        {
            layer = CalculateNextLayer(layer);
            return center + shiftFromCenter;
        }

        var point = CalculateCoordinates(layer, angle);

        angle = CalculateNextAngle(angle);
        if (ShouldUpdateLayer(angle))
        {
            layer = CalculateNextLayer(layer);
            angle %= FullCircleTurn;
        }

        return point + shiftFromCenter;
    }

    private Point CalculateCoordinates(int currentLayer, double currentAngle)
    {
        var x = center.X + currentLayer * DistanceLayersDifference * Math.Cos(currentAngle);
        var y = center.Y + currentLayer * DistanceLayersDifference * Math.Sin(currentAngle);
        return new Point((int) x, (int) y);
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