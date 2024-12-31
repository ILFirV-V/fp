using System.Drawing;
using FluentAssertions;
using TagsCloudContainer.TagsCloudVisualization.Extensions;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Providers;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.Tests.TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(Layouter))]
public partial class LayouterTests
{
    private IImageSettingsProvider imageSettingsProvider;
    private IRectanglePlacementStrategy rectanglePlacementStrategy;

    [SetUp]
    public void SetUp()
    {
        imageSettingsProvider = new ImageSettingsProvider();
        rectanglePlacementStrategy = new SpiralPlacementStrategy();
    }

    [Test]
    [TestCaseSource(nameof(zeroSizeCases))]
    public void PutNextRectangle_Should_ThrowsArgumentException(Size rectangleSize)
    {
        var circularCloudLayouter = new Layouter(imageSettingsProvider, rectanglePlacementStrategy);

        var action = () => circularCloudLayouter.PutNextRectangle(rectangleSize);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("Размер ширины и высоты должен быть больше 0.");
    }

    [Test]
    [TestCaseSource(nameof(unusualRectangleSizeCases))]
    [TestCaseSource(nameof(validRectangleSizeCases))]
    public void PutNextRectangle_Should_PositionsRectanglesOutsideCenter(IList<Size> sizes)
    {
        var imageSettings = imageSettingsProvider.GetImageSettings();
        var center = imageSettings.Size.Center();
        var circularCloudLayouter = new Layouter(imageSettingsProvider, rectanglePlacementStrategy);

        var rectangles = sizes.Select(size => circularCloudLayouter.PutNextRectangle(size)).ToList();

        rectangles.Skip(1).Should().NotContain(r => r.Contains(center));
    }

    [Test]
    [TestCaseSource(nameof(unusualRectangleSizeCases))]
    [TestCaseSource(nameof(validRectangleSizeCases))]
    public void PutNextRectangle_Should_ReturnsRectanglesWithCorrectSize(IList<Size> sizes)
    {
        var circularCloudLayouter = new Layouter(imageSettingsProvider, rectanglePlacementStrategy);

        var rectangles = sizes.Select(size => circularCloudLayouter.PutNextRectangle(size)).ToList();

        rectangles.Select(r => r.Size).Should().BeEquivalentTo(sizes);
    }

    [Test]
    [TestCaseSource(nameof(unusualRectangleSizeCases))]
    [TestCaseSource(nameof(validRectangleSizeCases))]
    public void PutNextRectangle_Should_ReturnsNonIntersectingRectangles(IList<Size> sizes)
    {
        var circularCloudLayouter = new Layouter(imageSettingsProvider, rectanglePlacementStrategy);
        var intersectingRectangles = new List<Rectangle>();

        var rectangles = sizes.Select(size => circularCloudLayouter.PutNextRectangle(size)).ToList();
        foreach (var checkRectangle in rectangles)
        {
            intersectingRectangles.AddRange(rectangles
                .Where(rectangle => checkRectangle != rectangle && rectangle.IntersectsWith(checkRectangle)));
        }

        intersectingRectangles.Should().BeEmpty();
    }

    [Test]
    [TestCaseSource(nameof(validRectangleSizeCases))]
    public void PutNextRectangle_Should_PlacesFirstRectangleInCenter(IList<Size> sizes)
    {
        var imageSettings = imageSettingsProvider.GetImageSettings();
        var center = imageSettings.Size.Center();
        var circularCloudLayouter = new Layouter(imageSettingsProvider, rectanglePlacementStrategy);

        var rectangles = sizes.Select(size => circularCloudLayouter.PutNextRectangle(size));

        rectangles.First().Center().Should().BeEquivalentTo(center);
    }
}