using System.Drawing;
using Autofac;
using FluentAssertions;
using TagsCloudContainer.ConsoleUi;
using TagsCloudContainer.ConsoleUi.Runner.Interfaces;
using TagsCloudContainer.ConsoleUi.Tuners;
using TagsCloudContainer.ConsoleUi.Tuners.Interfaces;

namespace TagsCloudContainer.Tests.ConsoleUiTests;

[TestFixture]
public partial class ConsoleClientTests
{
    private IContainer scope;

    [OneTimeSetUp]
    public void SetUp()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Tuner>().As<ITuner>();
        builder.RegisterModule(new ConsoleClientModule());
        scope = builder.Build();
    }

    [Test]
    [TestCaseSource(nameof(ContainsTestCases))]
    public void Run_ShouldContainsImage_AfterGenerate(string inputString, string resultOutputImagePath)
    {
        var runner = scope.Resolve<ITagsCloudContainerUi>();
        var tuner = scope.Resolve<ITuner>();

        tuner.Tune(inputString.Split());
        runner.Run();

        File.Exists(resultOutputImagePath).Should().BeTrue("Файл изображения должен существовать после генерации");
        if (File.Exists(resultOutputImagePath))
        {
            File.Delete(resultOutputImagePath);
        }
    }

    [Test]
    [TestCaseSource(nameof(EqualsTestCases))]
    public void Run_Should_EqualsExpectedImage(string inputString, string outputImagePath, string expectedImagePath)
    {
        var runner = scope.Resolve<ITagsCloudContainerUi>();
        var tuner = scope.Resolve<ITuner>();
        using var referenceImage = new Bitmap(expectedImagePath);

        tuner.Tune(inputString.Split());
        runner.Run();

        using var outputImage = new Bitmap(outputImagePath);
        IsImagesAreEqual(referenceImage, outputImage).Should().BeTrue("Пиксели изображений должны совпадать");
    }

    private static bool IsImagesAreEqual(Bitmap referenceImage, Bitmap outputImage)
    {
        if (!referenceImage.Size.Equals(outputImage.Size))
        {
            return false;
        }

        for (var x = 0; x < referenceImage.Width; x++)
        {
            for (var y = 0; y < referenceImage.Height; y++)
            {
                var referencePixel = referenceImage.GetPixel(x, y);
                var outputPixel = outputImage.GetPixel(x, y);
                if (!outputPixel.ToArgb().Equals(referencePixel.ToArgb()))
                {
                    return false;
                }
            }
        }

        return true;
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        scope.Dispose();
    }
}