using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.ConsoleUi.Runner;
using TagsCloudContainer.ConsoleUi.Runner.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Extensions;
using TagsCloudContainer.TextAnalyzer.Extensions;

namespace TagsCloudContainer.ConsoleUi;

public class ConsoleClientModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var services = new ServiceCollection();
        services.AddTextAnalyzerServices();
        services.AddTagsCloudVisualization();

        builder.Populate(services);

        builder.RegisterType<TagsCloudContainerUi>().As<ITagsCloudContainerUi>();
    }
}