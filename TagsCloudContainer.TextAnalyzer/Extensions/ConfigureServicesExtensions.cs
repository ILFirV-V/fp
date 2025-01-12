using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Filters;
using TagsCloudContainer.TextAnalyzer.Logic.Filters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Formatters;
using TagsCloudContainer.TextAnalyzer.Logic.Formatters.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Preprocessors;
using TagsCloudContainer.TextAnalyzer.Logic.Preprocessors.Interfaces;
using TagsCloudContainer.TextAnalyzer.Logic.Readers;
using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;
using TagsCloudContainer.TextAnalyzer.Providers;
using TagsCloudContainer.TextAnalyzer.Providers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddTextAnalyzerServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileTextReader, FileTextReader>();
        services.AddSingleton<IWordFormatter<WordDetails>, WordCaseFormatter>();
        services.AddSingleton<IWordFilter<WordDetails>, WordFilter>();
        services.AddSingleton<IWordReader, WordReader>();
        services.AddSingleton<IWordSettingsProvider, WordSettingsProvider>();
        services.AddSingleton<IAnalyzerFactory, AnalyzerFactory>();
        services.AddScoped<ITextPreprocessor, TextPreprocessor>();

        return services;
    }
}