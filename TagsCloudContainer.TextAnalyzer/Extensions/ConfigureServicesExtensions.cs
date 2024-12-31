using Microsoft.Extensions.DependencyInjection;
using MyStemWrapper;
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
        var resourceDirectory = Path.Combine(AppContext.BaseDirectory, "Resources");
        var myStemPath = Path.Combine(resourceDirectory, "mystem.exe");
        if (!File.Exists(myStemPath))
        {
            throw new FileNotFoundException($"Файл MyStem не найден по пути: {myStemPath}");
        }

        services.AddSingleton(new MyStem
        {
            PathToMyStem = myStemPath,
            Parameters = "-nli"
        });
        services.AddSingleton<IFileTextReader, FileTextReader>();
        services.AddScoped<IWordAnalyzer<WordDetails>, WordAnalyzer>();
        services.AddSingleton<IWordFormatter<WordDetails>, WordCaseFormatter>();
        services.AddSingleton<IWordFilter<WordDetails>, WordFilter>();
        services.AddSingleton<IWordReader, WordReader>();
        services.AddSingleton<IWordSettingsProvider, WordSettingsProvider>();
        services.AddScoped<ITextPreprocessor, TextPreprocessor>();

        return services;
    }
}