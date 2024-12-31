using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;

public class LayoutCreator(IServiceProvider serviceProvider) : ILayoutCreator
{
    public Result<ILayouter> GetLayouter()
    {
        var service = serviceProvider.GetService<ILayouter>();
        return service is null
            ? new Error($"Нет зарегистрированных сервисов: '{nameof(ILayouter)}'")
            : Result<ILayouter>.Ok(service);
    }
}