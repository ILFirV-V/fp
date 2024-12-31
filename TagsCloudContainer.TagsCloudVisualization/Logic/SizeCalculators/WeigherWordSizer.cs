using System.Collections.Immutable;
using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Models;
using TagsCloudContainer.TagsCloudVisualization.Providers.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators;

internal class WeigherWordSizer(IImageSettingsProvider imageSettingsProvider) : IWeigherWordSizer
{
    public IReadOnlyCollection<ViewWord> CalculateWordSizes(IReadOnlyDictionary<string, int> wordFrequencies,
        int minSize = 8, int maxSize = 24)
    {
        if (wordFrequencies.Count == 0)
        {
            return ImmutableArray<ViewWord>.Empty;
        }

        var maxFrequency = wordFrequencies.Values.Max();
        var wordSizes = new HashSet<ViewWord>();
        var settings = imageSettingsProvider.GetImageSettings();
        foreach (var entry in wordFrequencies)
        {
            if (entry.Value <= 0)
            {
                continue;
            }

            var viewWord = CreateViewWord(entry.Key, entry.Value, maxFrequency, minSize, maxSize, settings.FontFamily);
            wordSizes.Add(viewWord);
        }

        return wordSizes;
    }

    private static ViewWord CreateViewWord(string word, int frequency, int maxFrequency, int minSize, int maxSize,
        FontFamily fontFamily)
    {
        var normalizedFrequency = CalculateNormalizedFrequency(frequency, maxFrequency);
        var size = CalculateFontSize(minSize, maxSize, normalizedFrequency);
        var roundedSize = (int) Math.Round(size);
        var font = new Font(fontFamily, roundedSize);
        return new ViewWord(word, font);
    }

    private static double CalculateFontSize(int minSize, int maxSize, double normalizedFrequency)
    {
        return minSize + (maxSize - minSize) * normalizedFrequency;
    }

    private static double CalculateNormalizedFrequency(int wordFrequency, int maxFrequency)
    {
        return Math.Log10(wordFrequency + 1) / Math.Log10(maxFrequency + 1);
    }
}