using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;

public interface ILayouter
{
    public Rectangle PutNextRectangle(Size rectangleSize);
}