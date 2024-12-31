using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;

internal sealed class Layouter : ILayouter
{
    private List<Rectangle> Rectangles { get; } = [];
    private readonly IRectanglePlacementStrategy placementStrategy;
    private Point Center { get; }

    public Layouter(IImageSettingsProvider imageSettingsProvider, IRectanglePlacementStrategy placementStrategy)
    {
        var imageSettings = imageSettingsProvider.GetImageSettings();
        Center = new Point(imageSettings.Size.Width / 2, imageSettings.Size.Height / 2);
        this.placementStrategy = placementStrategy;
        this.placementStrategy.SetCenterPoint(Center);
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
        {
            throw new ArgumentException("Размер ширины и высоты должен быть больше 0.");
        }

        var rectangle = CreateNewRectangle(rectangleSize);
        rectangle = RectangleCompressions(rectangle);
        Rectangles.Add(rectangle);
        return rectangle;
    }

    private Rectangle CreateNewRectangle(Size rectangleSize)
    {
        var rectangle = new Rectangle(placementStrategy.GetNextRectangleLocation(rectangleSize), rectangleSize);
        while (CheckRectangleOverlaps(rectangle))
        {
            rectangle = new Rectangle(placementStrategy.GetNextRectangleLocation(rectangleSize), rectangleSize);
        }

        return rectangle;
    }

    private Rectangle RectangleCompressions(Rectangle rectangle)
    {
        var compressionRectangle = rectangle;
        compressionRectangle = Compression(compressionRectangle,
            moveRectangle => moveRectangle.X > Center.X,
            moveRectangle => moveRectangle.X + rectangle.Width < Center.X,
            (moveRectangle, direction) => moveRectangle with { X = moveRectangle.X + direction });

        compressionRectangle = Compression(compressionRectangle,
            moveRectangle => moveRectangle.Y > Center.Y,
            moveRectangle => moveRectangle.Y + moveRectangle.Height < Center.Y,
            (moveRectangle, direction) => moveRectangle with { Y = moveRectangle.Y + direction });

        return compressionRectangle;
    }

    private Rectangle Compression(Rectangle rectangle,
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

            if ((direction == -1 && !checkPositiveMove(moveRectangle))
                || (direction == 1 && !checkNegativeMove(moveRectangle)))
            {
                break;
            }
        }

        return moveRectangle;
    }

    private bool CheckRectangleOverlaps(Rectangle rectangle)
    {
        return Rectangles.Any(r => r.IntersectsWith(rectangle));
    }
}