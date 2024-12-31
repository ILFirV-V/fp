using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.WordAnalyzerTests;

public partial class WordAnalyzerTests
{
    private static IReadOnlyCollection<TestCaseData> validWordsTestCases =
    [
        new TestCaseData("тест", new WordDetails("тест", "тест", "S"))
            .SetName("Noun"),
        new TestCaseData("бегают", new WordDetails("бегают", "бегать", "V"))
            .SetName("Verb"),
        new TestCaseData("стекла", new WordDetails("стекла", "стекло", "S"))
            .SetName("NounWithMultipleForms"),
        new TestCaseData("хороший", new WordDetails("хороший", "хороший", "A"))
            .SetName("Adjective"),
        new TestCaseData("быстро", new WordDetails("быстро", "быстро", "ADV"))
            .SetName("Adverb"),
        new TestCaseData("три", new WordDetails("три", "три", "NUM"))
            .SetName("Numeral")
    ];

    private static IReadOnlyCollection<TestCaseData> notValidWordsTestCases =
    [
        new TestCaseData("")
            .SetName("EmptyWord"),
        new TestCaseData("   ")
            .SetName("WhitespaceWord"),
        new TestCaseData("#$%^")
            .SetName("WordWithSymbols"),
        new TestCaseData("несуществующееслово123")
            .SetName("NonExistingWord")
    ];
}