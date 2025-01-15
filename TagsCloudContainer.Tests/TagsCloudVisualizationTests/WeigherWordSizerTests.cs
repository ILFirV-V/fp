using FluentAssertions;
using TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators;
using TagsCloudContainer.TagsCloudVisualization.Providers;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.Tests.TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(WeigherWordSizer))]
public partial class WeigherWordSizerTests
{
    private IImageSettingsProvider imageSettingsProvider;

    [SetUp]
    public void SetUp()
    {
        imageSettingsProvider = new ImageSettingsProvider();
    }

    [Test]
    [TestCaseSource(nameof(emptyResultCases))]
    public void CalculateWordSizes_Should_ReturnsEmptyCollection(
        IReadOnlyDictionary<string, int> wordFrequencies)
    {
        var calculator = new WeigherWordSizer(imageSettingsProvider);

        var result = calculator.CalculateWordSizes(wordFrequencies);

        result.GetValueOrThrow().Should().BeEmpty();
    }

    [Test]
    [TestCaseSource(nameof(validWordCountsCases))]
    public void WeigherWordSizer_Should_CompareWordCountToWordFont(
        IReadOnlyDictionary<string, int> wordFrequencies)
    {
        var calculator = new WeigherWordSizer(imageSettingsProvider);
        var expectedWordOrder = wordFrequencies.OrderByDescending(x => x.Value).Select(x => x.Key);

        var result = calculator.CalculateWordSizes(wordFrequencies);

        var resultWordOrder = result.GetValueOrThrow().OrderByDescending(w => w.Font.Size).Select(w => w.Word);
        resultWordOrder.Should().BeEquivalentTo(expectedWordOrder);
    }

    [Test]
    [TestCaseSource(nameof(customMinMaxSizeCases))]
    public void CalculateWordSizes_Should_UsesCustomMinMaxSizes(IReadOnlyDictionary<string, int> wordFrequencies,
        int minSize, int maxSize)
    {
        var calculator = new WeigherWordSizer(imageSettingsProvider);

        var result = calculator.CalculateWordSizes(wordFrequencies, minSize, maxSize);

        result.GetValueOrThrow().Should().AllSatisfy(x => x.Font.Size.Should().BeInRange(minSize, maxSize));
    }

    [Test]
    [TestCaseSource(nameof(customMinMaxSizeCases))]
    public void CalculateWordSizes_Should_ContainsMaxSize(IReadOnlyDictionary<string, int> wordFrequencies, int minSize,
        int maxSize)
    {
        var calculator = new WeigherWordSizer(imageSettingsProvider);

        var result = calculator.CalculateWordSizes(wordFrequencies, minSize, maxSize);

        result.GetValueOrThrow().Max(w => w.Font.Size).Should().Be(maxSize);
    }

    [Test]
    [TestCaseSource(nameof(errorCustomMinMaxSizeCases))]
    public void CalculateWordSizes_ShouldWithError_WhereErrorSizes(IReadOnlyDictionary<string, int> wordFrequencies,
        int minSize, int maxSize)
    {
        var calculator = new WeigherWordSizer(imageSettingsProvider);

        var result = calculator.CalculateWordSizes(wordFrequencies, minSize, maxSize);

        result.Error.Should().NotBeNull();
    }
}