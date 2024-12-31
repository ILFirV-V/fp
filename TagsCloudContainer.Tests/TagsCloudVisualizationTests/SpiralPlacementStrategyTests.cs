using System.Drawing;
using FluentAssertions;
using TagsCloudContainer.TagsCloudVisualization.Logic.Strategies;

namespace TagsCloudContainer.Tests.TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(SpiralPlacementStrategy))]
public class SpiralPlacementStrategyTests
{
    private static readonly IReadOnlyCollection<TestCaseData> zeroSizeCases =
    [
        new TestCaseData(new Size(0, 0))
            .SetName("AllZero"),
        new TestCaseData(new Size(1, 0))
            .SetName("WidthZero"),
        new TestCaseData(new Size(0, 1))
            .SetName("HeightZero")
    ];

    private static readonly IReadOnlyCollection<TestCaseData> validRectangleSizeCases =
    [
        new TestCaseData(new List<Size> { new(10, 10) })
            .SetName("OneSize"),
        new TestCaseData(new List<Size> { new(10, 10), new(20, 20), new(15, 15), new(5, 7), new(3, 1), new(15, 35) })
            .SetName("MoreSizes")
    ];

    private SpiralPlacementStrategy strategy;
    private Point center;

    [SetUp]
    public void Setup()
    {
        strategy = new SpiralPlacementStrategy();
        center = new Point(100, 100);
        strategy.SetCenterPoint(center);
    }

    [Test]
    [TestCaseSource(nameof(validRectangleSizeCases))]
    public void PutNextRectangle_Should_PlacesFirstPositionsInCenter(ICollection<Size> sizes)
    {
        var points = new List<Point>(sizes.Count);
        var firstRectangleLocation = center - sizes.First() / 2;

        points.AddRange(sizes.Select(size => strategy.GetNextRectangleLocation(size)));

        points.First().Should().BeEquivalentTo(firstRectangleLocation);
    }

    [Test]
    [TestCaseSource(nameof(zeroSizeCases))]
    public void PutNextRectangle_Should_ThrowsArgumentException(Size rectangleSize)
    {
        var action = () => strategy.GetNextRectangleLocation(rectangleSize);

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("Размер ширины и высоты должен быть больше 0.");
    }

    [Test]
    [TestCaseSource(nameof(validRectangleSizeCases))]
    public void PutNextRectangle_Should_PositionsOutsideCenter(IList<Size> sizes)
    {
        var firstRectangleSize = sizes.First();
        var firstRectangle = new Rectangle(center + firstRectangleSize / 2, firstRectangleSize);

        var rectangleLocations = sizes.Select(size => strategy.GetNextRectangleLocation(size)).ToList();

        rectangleLocations.Skip(1).Should().NotContain(r => firstRectangle.Contains(r));
    }
}