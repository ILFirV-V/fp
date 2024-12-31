using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators;
using TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers;
using TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Providers;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddTagsCloudVisualization(this IServiceCollection services)
    {
        services.AddTransient<ILayouter, Layouter>();
        services.AddTransient<IRectanglePlacementStrategy, SpiralPlacementStrategy>();
        services.AddScoped<IWordsCloudVisualizer, WordsCloudVisualizer>();
        services.AddScoped<IWeigherWordSizer, WeigherWordSizer>();
        services.AddSingleton<IFileSettingsProvider, FileSettingsProvider>();
        services.AddSingleton<IImageSettingsProvider, ImageSettingsProvider>();
        services.AddSingleton<ILayoutCreator, LayoutCreator>();
        return services;
    }
}