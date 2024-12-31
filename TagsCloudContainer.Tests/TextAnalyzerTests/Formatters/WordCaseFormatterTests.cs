using FluentAssertions;
using TagsCloudContainer.TextAnalyzer.Logic.Formatters;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Formatters;

[TestFixture]
[TestOf(typeof(WordCaseFormatter))]
public class WordCaseFormatterTests
{
    private static readonly string startWordFake = string.Empty;
    private static readonly string wordSpeechPartFake = string.Empty;

    private static IReadOnlyCollection<TestCaseData> validTestCases =
    [
        new TestCaseData(string.Empty, string.Empty)
            .SetName("EmptyFormatedWord"),
        new TestCaseData("TESTWORD", "testword")
            .SetName("UppercaseFormatedWord"),
        new TestCaseData("TestWord", "testword")
            .SetName("MixedCaseFormated"),
        new TestCaseData("testword", "testword")
            .SetName("Lowercase FormatedWord"),
        new TestCaseData("123Test", "123test")
            .SetName("NumbersAndLetters"),
        new TestCaseData("Тест", "тест")
            .SetName("CyrillicLetters")
    ];

    [Test]
    [TestCaseSource(nameof(validTestCases))]
    public void Format_Should_ReturnsExpectedFormatedWord(string startFormatedWord,
        string expectedFormatedWord)
    {
        var wordDetails = new WordDetails(startWordFake, startFormatedWord, wordSpeechPartFake);
        var expectedWordDetails = wordDetails with { FormatedWord = expectedFormatedWord };
        var formatter = new WordCaseFormatter();

        var result = formatter.Format(wordDetails);

        result.Should().BeEquivalentTo(expectedWordDetails);
    }
}