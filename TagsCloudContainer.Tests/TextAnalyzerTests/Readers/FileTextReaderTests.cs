using FluentAssertions;
using TagsCloudContainer.TextAnalyzer.Logic.Readers;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Readers;

[TestFixture]
public class FileTextReaderTests
{
    [Test]
    public void ReadText_ShouldTextString_WhenExistingFile()
    {
        var fullPath = Path.GetTempFileName();
        const string expectedText = "This is a test file.";
        var reader = new FileTextReader();
        string actualText;

        try
        {
            File.WriteAllText(fullPath, expectedText);
            actualText = reader.ReadText(fullPath);
        }
        finally
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        actualText.Should().Be(expectedText);
    }

    [Test]
    public void ReadText_ShouldEmptyString_WhenFileDoesNotExist()
    {
        var reader = new FileTextReader();

        var actualText = reader.ReadText("nonexistent_file.txt");

        actualText.Should().BeEmpty();
    }
}