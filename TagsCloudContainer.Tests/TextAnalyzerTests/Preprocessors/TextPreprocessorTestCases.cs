using TagsCloudContainer.TextAnalyzer.Models;

namespace TagsCloudContainer.Tests.TextAnalyzerTests.Preprocessors;

public partial class TextPreprocessorTests
{
    private static IEnumerable<TestCaseData> ValidTestCases()
    {
        var myStemPath = GetMyStemPath();
        yield return new TestCaseData(string.Empty, new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int>())
            .SetName("Empty");
        yield return new TestCaseData("привет", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "привет", 1 } })
            .SetName("SingleWord");
        yield return new TestCaseData("яблоко\nяблоко\nтест\nяблоко", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "яблоко", 3 }, { "тест", 1 } })
            .SetName("MultipleSameWords");
        yield return new TestCaseData("Привет,\nмир!", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "привет", 1 }, { "мир", 1 } })
            .SetName("WithPunctuation");
        yield return new TestCaseData("Привет\nпривет\nПРИВЕТ", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "привет", 3 } })
            .SetName("DifferentCaseswords");
        yield return new TestCaseData("бежать\nбегущий\nбежал", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "бежать", 3 } })
            .SetName("DifferentFormsVerbs");
        yield return new TestCaseData("красный\nбыстро\nхорошо", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "красный", 1 }, { "быстро", 1 }, { "хорошо", 1 } })
            .SetName("DifferentPartsSpeech");
        yield return new TestCaseData("яблоко\nбежит",
                new WordSettings { ValidSpeechParts = ["S"], MyStemPath = myStemPath },
                new Dictionary<string, int> { { "яблоко", 1 } })
            .SetName("OnlyNoun");
        yield return new TestCaseData("яблоко\nбежит",
                new WordSettings { ValidSpeechParts = ["V"], MyStemPath = myStemPath },
                new Dictionary<string, int> { { "бежать", 1 } })
            .SetName("OnlyVerb");
        yield return new TestCaseData("быстро", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "быстро", 1 } })
            .SetName("Adverb");
        yield return new TestCaseData("два", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "два", 1 } })
            .SetName("Number");
        yield return new TestCaseData("красный", new WordSettings { MyStemPath = myStemPath },
                new Dictionary<string, int> { { "красный", 1 } })
            .SetName("Adjective");
    }


    private static string GetMyStemPath()
    {
        var resourceDirectory = Path.Combine(AppContext.BaseDirectory, "Resources");
        var myStemPath = Path.Combine(resourceDirectory, "mystem.exe");
        return myStemPath;
    }
}