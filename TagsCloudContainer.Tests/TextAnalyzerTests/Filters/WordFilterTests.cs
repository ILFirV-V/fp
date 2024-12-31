using FluentAssertions;
using TagsCloudContainer.TextAnalyzer.Logic.Filters;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Filters;

[TestFixture]
[TestOf(typeof(WordFilter))]
public class WordFilterTests
{
    private static readonly WordSettings defaultSettings = new();
    private static readonly string startWordFake = string.Empty;
    private static readonly string formatedWordFake = string.Empty;

    private static IReadOnlyCollection<TestCaseData> validTestCases =
    [
        new TestCaseData("V", defaultSettings)
            .SetName("Verb"),
        new TestCaseData("S", defaultSettings)
            .SetName("Noun"),
        new TestCaseData("A", defaultSettings)
            .SetName("Adjective"),
        new TestCaseData("ADV", defaultSettings)
            .SetName("Adverb"),
        new TestCaseData("NUM", defaultSettings)
            .SetName("Numeral"),
        new TestCaseData("N", new WordSettings { ValidSpeechParts = ["N"] })
            .SetName("CustomSettings")
    ];

    private static IEnumerable<TestCaseData> notValidTestCases =
    [
        new TestCaseData("invalid", defaultSettings)
            .SetName("InvalidSpeechPart"),
        new TestCaseData(null, defaultSettings)
            .SetName("NullSpeechPart"),
        new TestCaseData(" ", defaultSettings)
            .SetName("WhitespaceSpeechPart"),
        new TestCaseData("V", new WordSettings { ValidSpeechParts = ["S"] })
            .SetName("WithoutSpeechPart")
    ];

    [Test]
    [TestCaseSource(nameof(validTestCases))]
    public void FilterAvailableByPartSpeech_ShouldIsSuccess_BeVerify(string expectedSpeechPart, WordSettings settings)
    {
        var filter = new WordFilter();
        var wordDetails = new WordDetails(startWordFake, formatedWordFake, expectedSpeechPart);

        var result = filter.FilterAvailableByPartSpeech(wordDetails, settings);

        result.IsSuccess.Should().BeTrue();
    }

    [Test]
    [TestCaseSource(nameof(notValidTestCases))]
    public void FilterAvailableByPartSpeech_ShouldIsFail_BeNotVerify(string expectedSpeechPart, WordSettings settings)
    {
        var filter = new WordFilter();
        var wordDetails = new WordDetails(startWordFake, formatedWordFake, expectedSpeechPart);

        var result = filter.FilterAvailableByPartSpeech(wordDetails, settings);

        result.IsFail.Should().BeTrue();
    }
}