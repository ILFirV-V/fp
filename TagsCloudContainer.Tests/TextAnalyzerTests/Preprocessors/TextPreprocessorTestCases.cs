using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Preprocessors;

public partial class TextPreprocessorTests
{
    private static IReadOnlyCollection<TestCaseData> validTestCases =
    [
        new TestCaseData(string.Empty, new WordSettings(), new Dictionary<string, int>())
            .SetName("Empty"),
        new TestCaseData("привет", new WordSettings(), new Dictionary<string, int> { { "привет", 1 } })
            .SetName("SingleWord"),
        new TestCaseData("яблоко\nяблоко\nтест\nяблоко", new WordSettings(),
                new Dictionary<string, int> { { "яблоко", 3 }, { "тест", 1 } })
            .SetName("MultipleSameWords"),
        new TestCaseData("Привет,\nмир!", new WordSettings(),
                new Dictionary<string, int> { { "привет", 1 }, { "мир", 1 } })
            .SetName("WithPunctuation"),
        new TestCaseData("Привет\nпривет\nПРИВЕТ", new WordSettings(),
                new Dictionary<string, int> { { "привет", 3 } })
            .SetName("DifferentCaseswords"),
        new TestCaseData("бежать\nбегущий\nбежал", new WordSettings(),
                new Dictionary<string, int> { { "бежать", 3 } })
            .SetName("DifferentFormsVerbs"),
        new TestCaseData("красный\nбыстро\nхорошо", new WordSettings(),
                new Dictionary<string, int> { { "красный", 1 }, { "быстро", 1 }, { "хорошо", 1 } })
            .SetName("DifferentPartsSpeech"),
        new TestCaseData("яблоко\nбежит", new WordSettings() { ValidSpeechParts = ["S"] },
                new Dictionary<string, int> { { "яблоко", 1 } })
            .SetName("OnlyNoun"),
        new TestCaseData("яблоко\nбежит", new WordSettings() { ValidSpeechParts = ["V"] },
                new Dictionary<string, int> { { "бежать", 1 } })
            .SetName("OnlyVerb"),
        new TestCaseData("быстро", new WordSettings(), new Dictionary<string, int> { { "быстро", 1 } })
            .SetName("Adverb"),
        new TestCaseData("два", new WordSettings(), new Dictionary<string, int> { { "два", 1 } })
            .SetName("Number"),
        new TestCaseData("красный", new WordSettings(), new Dictionary<string, int> { { "красный", 1 } })
            .SetName("Adjective")
    ];
}