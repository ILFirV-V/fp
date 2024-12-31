using System.Drawing;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;

public interface ILayouter
{
    public Result<Rectangle> PutNextRectangle(Size rectangleSize);
}