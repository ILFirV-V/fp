using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;

public class LayoutCreator(IServiceProvider serviceProvider) : ILayoutCreator
{
    public ILayouter? GetOrNull()
    {
        return serviceProvider.GetService<ILayouter>();
    }
}