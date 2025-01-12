namespace TagsCloudContainer.Tests.ConsoleUiTests;

public partial class ConsoleClientTests
{
    private static IEnumerable<TestCaseData> ContainsTestCases()
    {
        var myStemPath = GetMyStemPath();
        var testDirectory = Path.GetTempPath();
        var filePath = GetTestFilePath();
        const string imageFileName = "testResultImage";
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}",
                GetImagePath(testDirectory, imageFileName, "png"))
            .SetName("BaseCommands");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName} -e jpg",
                GetImagePath(testDirectory, imageFileName, "jpeg"))
            .SetName("JpgCommands");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName} -e png -p S V A",
                GetImagePath(testDirectory, imageFileName, "png"))
            .SetName("PngCommandsWithWordSettings");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName} -p S V A ADV NUM -w 800 -h 600 -b White -c Black -f Arial",
                GetImagePath(testDirectory, imageFileName, "png"))
            .SetName("BaseCommandsWithImageSettings");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName} -e jpeg -b Red -c Blue",
                GetImagePath(testDirectory, imageFileName, "jpeg"))
            .SetName("JpegCommandsWithImageSettings");
    }

    private static IEnumerable<TestCaseData> EqualsTestCases()
    {
        var myStemPath = GetMyStemPath();
        var testDirectory = Path.GetTempPath();
        var sourceDirectory = GetTestDataDirectory();
        var filePath = GetTestFilePath();
        const string imageFileName = "testEqualsResultImage";
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}JpgCommands -e jpg",
                GetImagePath(testDirectory, $"{imageFileName}JpgCommands", "jpeg"),
                GetImagePath(sourceDirectory, "result", "jpeg"))
            .SetName("JpgCommands");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}PngCommandsWithWordSettings -p S V A",
                GetImagePath(testDirectory, $"{imageFileName}PngCommandsWithWordSettings", "png"),
                GetImagePath(sourceDirectory, "resultVSA", "png"))
            .SetName("PngCommandsWithWordSettings");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}PngCommandsOnlyNouns -e png -p S",
                GetImagePath(testDirectory, $"{imageFileName}PngCommandsOnlyNouns", "png"),
                GetImagePath(sourceDirectory, "resultS", "png"))
            .SetName("PngCommandsOnlyNouns");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}BaseCommandsWithImageSettings -e png -p S V A ADV NUM -w 800 -h 600 -b White -c Black -f Arial",
                GetImagePath(testDirectory, $"{imageFileName}BaseCommandsWithImageSettings", "png"),
                GetImagePath(sourceDirectory, "resultImageSettings", "png"))
            .SetName("BaseCommandsWithImageSettings");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}PngCommandsWithRedBackAndBlueWords -e png -w 800 -h 600 -b Red -c Blue -f Arial",
                GetImagePath(testDirectory, $"{imageFileName}PngCommandsWithRedBackAndBlueWords", "png"),
                GetImagePath(sourceDirectory, "resultRedBlue", "png"))
            .SetName("PngCommandsWithRedBackAndBlueWords");
        yield return new TestCaseData(
                $"-m {myStemPath} -i {filePath} -o {testDirectory} -n {imageFileName}BaseCommands",
                GetImagePath(testDirectory, $"{imageFileName}BaseCommands", "png"),
                GetImagePath(sourceDirectory, "result", "png"))
            .SetName("BaseCommands");
    }

    private static string GetTestFilePath()
    {
        var resourceDirectory = GetTestDataDirectory();
        var testPath = Path.Combine(resourceDirectory, "test.txt");
        return testPath;
    }

    private static string GetTestDataDirectory()
    {
        var dataDirectory = Path.Combine(AppContext.BaseDirectory, "ConsoleUiTests");
        dataDirectory = Path.Combine(dataDirectory, "TestData");
        return dataDirectory;
    }

    private static string GetMyStemPath()
    {
        var resourceDirectory = Path.Combine(AppContext.BaseDirectory, "Resources");
        var myStemPath = Path.Combine(resourceDirectory, "mystem.exe");
        return myStemPath;
    }

    private static string GetImagePath(string dataDirectory, string imageFileName, string imageFormat)
    {
        return Path.Combine(dataDirectory, $"{imageFileName}.{imageFormat}");
    }
}