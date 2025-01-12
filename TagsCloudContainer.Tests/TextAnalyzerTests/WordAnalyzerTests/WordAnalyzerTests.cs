using FluentAssertions;
using MyStemWrapper;
using TagsCloudContainer.TextAnalyzer.Logic.Analyzers;
using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.WordAnalyzerTests;

[TestFixture]
[TestOf(typeof(WordAnalyzer))]
public partial class WordAnalyzerTests
{
    private MyStem myStem;

    [SetUp]
    public void SetUp()
    {
        var resourceDirectory = Path.Combine(AppContext.BaseDirectory, "Resources");
        var myStemPath = Path.Combine(resourceDirectory, "mystem.exe");
        if (!File.Exists(myStemPath))
        {
            throw new FileNotFoundException($"Файл MyStem не найден по пути: {myStemPath}");
        }

        myStem = new MyStem
        {
            PathToMyStem = myStemPath,
            Parameters = "-nli"
        };
    }

    [Test]
    [TestCaseSource(nameof(validWordsTestCases))]
    public void TryAnalyzeWord_Should_HaveExpectedWordDetails(string word, WordDetails expectedWordDetails)
    {
        var analyzer = new WordAnalyzer(myStem);

        _ = analyzer.TryAnalyzeWord(word, out var result);

        result.Should().BeEquivalentTo(expectedWordDetails);
    }

    [Test]
    [TestCaseSource(nameof(notValidWordsTestCases))]
    public void TryAnalyzeWord_ShouldFalse_WhereError(string word)
    {
        var analyzer = new WordAnalyzer(myStem);

        var isAnalyze = analyzer.TryAnalyzeWord(word, out _);

        isAnalyze.Should().BeFalse();
    }
}