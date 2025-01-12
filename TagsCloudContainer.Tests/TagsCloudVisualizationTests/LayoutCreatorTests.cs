using FakeItEasy;
using FluentAssertions;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;

namespace TagsCloudContainer.Tests.TagsCloudVisualizationTests;

[TestFixture]
[TestOf(typeof(LayoutCreator))]
public class LayoutCreatorTests
{
    [Test]
    public void GetService_ShouldBeCalledTwice_WhenGetOrNullIsInvokedTwiceTimes()
    {
        var fakeLayouter = A.Fake<ILayouter>();
        var fakeServiceProvider = A.Fake<IServiceProvider>();
        A.CallTo(() => fakeServiceProvider.GetService(typeof(ILayouter))).Returns(fakeLayouter);
        var layoutCreator = new LayoutCreator(fakeServiceProvider);

        _ = layoutCreator.GetLayouter();
        _ = layoutCreator.GetLayouter();

        A.CallTo(() => fakeServiceProvider.GetService(typeof(ILayouter)))
            .MustHaveHappenedTwiceExactly();
    }

    [Test]
    public void GetLayouter_ShouldIsFail_WhenLayouterIsNotRegistered()
    {
        var fakeServiceProvider = A.Fake<IServiceProvider>();
        A.CallTo(() => fakeServiceProvider.GetService(typeof(ILayouter))).Returns(null);
        var layoutCreator = new LayoutCreator(fakeServiceProvider);

        var layouter = layoutCreator.GetLayouter();

        layouter.IsFail.Should().BeTrue();
    }
}