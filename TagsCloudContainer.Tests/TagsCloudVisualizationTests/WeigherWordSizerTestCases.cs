using System.Collections.Immutable;

namespace TagsCloudContainer.Tests.TagsCloudVisualizationTests;

public partial class WeigherWordSizerTests
{
    private static readonly IReadOnlyCollection<TestCaseData> errorCustomMinMaxSizeCases =
    [
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, 0, 30)
            .SetName("ZeroMinSize"),
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, 0, 0)
            .SetName("AllZeroSizes"),
        new TestCaseData(new Dictionary<string, int> { { "test", 100 }, { "another", 50 } }, 5, 1)
            .SetName("MinSizeMoreMaxSize"),
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, -2, -1)
            .SetName("NegativeSizes"),
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, -2, 1)
            .SetName("NegativeMinSize"),
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, -2, 0)
            .SetName("NegativeMinSizeZeroMaxSize"),
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, -1, -2)
            .SetName("MinSizeMoreMaxSizeWhereAllNegative")
    ];

    private static readonly IReadOnlyCollection<TestCaseData> customMinMaxSizeCases =
    [
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }, 10, 30).SetName("Min10Max30"),
        new TestCaseData(new Dictionary<string, int> { { "test", 100 }, { "another", 50 } }, 5, 25).SetName("Min5Max25")
    ];

    private static readonly IReadOnlyCollection<TestCaseData> validWordCountsCases =
    [
        new TestCaseData(new Dictionary<string, int> { { "test", 1 } }).SetName("OneWord"),
        new TestCaseData(new Dictionary<string, int> { { "test", 10 } }).SetName("OneWordType"),
        new TestCaseData(new Dictionary<string, int> { { "test", 100 }, { "another", 50 } }).SetName("MoreWords")
    ];

    private static readonly IReadOnlyCollection<TestCaseData> emptyResultCases =
    [
        new TestCaseData(new Dictionary<string, int> { { "test", -1 } }).SetName("InvalidWordCount"),
        new TestCaseData(new Dictionary<string, int> { { "test", 0 } }).SetName("ZeroWordCount"),
        new TestCaseData(ImmutableDictionary<string, int>.Empty).SetName("Empty")
    ];
}