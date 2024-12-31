using System.Collections.Immutable;
using FluentAssertions;
using TagsCloudContainer.TextAnalyzer.Logic.Readers;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Readers;

[TestFixture]
[TestOf(typeof(WordReader))]
public class WordReaderTests
{
    private static readonly IReadOnlyCollection<TestCaseData> testCases =
    [
        new TestCaseData(string.Empty, ImmutableArray<string>.Empty)
            .SetName("EmptyText"),
        new TestCaseData("   ", ImmutableArray<string>.Empty)
            .SetName("WhitespaceText"),
        new TestCaseData("test", new[] { "test" })
            .SetName("SingleWord"),
        new TestCaseData("test word another", new[] { "test word another" })
            .SetName("MultiWord"),
        new TestCaseData(" test  word   another ", new[] { "test  word   another" })
            .SetName("MultiWordWithSpaces"),
        new TestCaseData("test;word,another", new[] { "test;word,another" })
            .SetName("MultiWordWithSpacesWithSpaces"),
        new TestCaseData("test\nword\nanother", new[] { "test", "word", "another" })
            .SetName("MultiWordWithNewLines"),
        new TestCaseData("test\r\nword\r\nanother", new[] { "test", "word", "another" })
            .SetName("MultiWordWithNewLinesWithSpaces")
    ];

    [Test]
    [TestCaseSource(nameof(testCases))]
    public void ReadWords_Should_EquivalentToExpectationResult(string text, IEnumerable<string> expectedResult)
    {
        var reader = new WordReader();

        var result = reader.ReadWords(text);

        result.Should().BeEquivalentTo(expectedResult);
    }
}