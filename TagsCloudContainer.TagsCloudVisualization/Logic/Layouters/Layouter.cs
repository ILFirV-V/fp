using System.Drawing;
using TagsCloudContainer.Core;
using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;

internal sealed class Layouter : ILayouter
{
    private readonly List<Rectangle> rectangles = [];
    private readonly IRectanglePlacementStrategy placementStrategy;
    private readonly ImageSettings imageSettings;
    private Point Center { get; }

    public Layouter(IImageSettingsProvider imageSettingsProvider, IRectanglePlacementStrategy placementStrategy)
    {
        imageSettings = imageSettingsProvider.GetImageSettings();
        Center = new Point(imageSettings.Size.Width / 2, imageSettings.Size.Height / 2);
        this.placementStrategy = placementStrategy;
        this.placementStrategy.SetCenterPoint(Center);
    }

    public Result<Rectangle> PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
        {
            return new Error("Размер ширины и высоты должен быть больше 0.");
        }

        return CreateNewRectangle(rectangleSize)
            .Then(RectangleCompressions)
            .Then(ValidateRectangleBounds)
            .Then(rectangle =>
            {
                rectangles.Add(rectangle);
                return rectangle;
            });
    }

    private Result<Rectangle> ValidateRectangleBounds(Rectangle rectangle)
    {
        var imageWidth = imageSettings.Size.Width;
        var imageHeight = imageSettings.Size.Height;
        if (rectangle.Left < 0 || rectangle.Top < 0 ||
            rectangle.Right > imageWidth || rectangle.Bottom > imageHeight)
        {
            return new Error("Прямоугольник выходит за границы изображения.");
        }

        return rectangle;
    }

    private Result<Rectangle> CreateNewRectangle(Size rectangleSize)
    {
        Result<Rectangle> rectangleResult;
        return Result<Rectangle>.Of(() =>
        {
            do
            {
                rectangleResult = placementStrategy.GetNextRectangleLocation(rectangleSize)
                    .Then(point => new Rectangle(point, rectangleSize));
            } while (rectangleResult.Then(CheckRectangleOverlaps).GetValueOrThrow());

            return rectangleResult.GetValueOrThrow();
        });
    }

    private Result<Rectangle> RectangleCompressions(Rectangle rectangle)
    {
        return CompressRectangle(rectangle,
                moveRectangle => moveRectangle.X > Center.X,
                moveRectangle => moveRectangle.X + rectangle.Width < Center.X,
                (moveRectangle, direction) => moveRectangle with { X = moveRectangle.X + direction })
            .Then(compressionRectangle => CompressRectangle(compressionRectangle,
                moveRectangle => moveRectangle.Y > Center.Y,
                moveRectangle => moveRectangle.Y + moveRectangle.Height < Center.Y,
                (moveRectangle, direction) => moveRectangle with { Y = moveRectangle.Y + direction })
            );
    }

    private Result<Rectangle> CompressRectangle(Rectangle rectangle,
        Func<Rectangle, bool> checkPositiveMove,
        Func<Rectangle, bool> checkNegativeMove,
        Func<Rectangle, int, Rectangle> doMove)
    {
        if (checkPositiveMove(rectangle) == checkNegativeMove(rectangle))
        {
            return rectangle;
        }

        var direction = checkPositiveMove(rectangle) ? -1 : 1;
        var moveRectangle = rectangle;
        while (true)
        {
            moveRectangle = doMove(moveRectangle, direction);
            if (CheckRectangleOverlaps(moveRectangle))
            {
                moveRectangle = doMove(moveRectangle, -1 * direction);
                break;
            }

            if ((direction == -1 && !checkPositiveMove(moveRectangle)) ||
                (direction == 1 && !checkNegativeMove(moveRectangle)))
            {
                break;
            }
        }

        return moveRectangle;
    }

    private bool CheckRectangleOverlaps(Rectangle rectangle)
    {
        return rectangles.Any(r => r.IntersectsWith(rectangle));
    }
}