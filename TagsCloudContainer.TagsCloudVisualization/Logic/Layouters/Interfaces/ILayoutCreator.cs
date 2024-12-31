using TagsCloudContainer.Core;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;

public interface ILayoutCreator
{
    public Result<ILayouter> GetLayouter();
}