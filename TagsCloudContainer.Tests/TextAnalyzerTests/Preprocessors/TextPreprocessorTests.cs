using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.TextAnalyzer.Extensions;
using TagsCloudContainer.TextAnalyzer.Logic.Preprocessors;
using TagsCloudContainer.TextAnalyzer.Logic.Preprocessors.Interfaces;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Preprocessors;

[TestFixture]
[TestOf(typeof(TextPreprocessor))]
public partial class TextPreprocessorTests
{
    private ITextPreprocessor textPreprocessor;

    [OneTimeSetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        services.AddTextAnalyzerServices();
        var builder = new ContainerBuilder();
        builder.Populate(services);
        var app = builder.Build();
        using var scope = app.BeginLifetimeScope();
        textPreprocessor = scope.Resolve<ITextPreprocessor>();
    }

    [Test]
    [TestCaseSource(nameof(validTestCases))]
    public void GetWordFrequencies_Should_EqualToExpectedWordFrequencies(string text, WordSettings settings,
        IReadOnlyDictionary<string, int> wordFrequencies)
    {
        var result = textPreprocessor.GetWordFrequencies(text, settings);

        result.Should().BeEquivalentTo(wordFrequencies);
    }
}