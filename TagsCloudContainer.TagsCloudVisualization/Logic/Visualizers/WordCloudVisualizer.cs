using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Extensions;
using TagsCloudContainer.TagsCloudVisualization.Logic.Layouters.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Models;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers;

public class WordsCloudVisualizer(ILayoutCreator containerCreator, IWeigherWordSizer weigherWordSizer)
    : IWordsCloudVisualizer
{
    public void SaveImage(Image image, FileSettings settings)
    {
        if (!Directory.Exists(settings.OutputPath))
        {
            throw new DirectoryNotFoundException($"Output directory doesn't exist: {settings.OutputPath}");
        }

        var fileNameWithFormat = $"{settings.OutputFileName}{settings.ImageFormat.FileExtensionFromToString()}";
        var path = Path.Combine(settings.OutputPath, fileNameWithFormat);
        image.Save(path, settings.ImageFormat);
    }

    public Image CreateImage(ImageSettings settings, IReadOnlyDictionary<string, int> wordCounts)
    {
        var viewWords = weigherWordSizer.CalculateWordSizes(wordCounts);
        var bitmap = CreateImageMap(settings);
        var image = VisualizeWords(bitmap, viewWords, settings);
        return image;
    }

    private static Bitmap CreateImageMap(ImageSettings imageSettings)
    {
        var bitmap = new Bitmap(imageSettings.Size.Width, imageSettings.Size.Height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(imageSettings.BackgroundColor);
        return bitmap;
    }

    private Bitmap VisualizeWords(Bitmap bitmap, IReadOnlyCollection<ViewWord> viewWords, ImageSettings imageSettings)
    {
        var container = containerCreator.GetOrNull() ?? throw new ArgumentNullException(nameof(containerCreator));
        using var graphics = Graphics.FromImage(bitmap);
        foreach (var viewWord in viewWords)
        {
            var textSize = CalculateWordSize(graphics, viewWord);
            var rectangle = container.PutNextRectangle(textSize);
            DrawTextInRectangle(graphics, viewWord, rectangle, imageSettings);
        }

        return bitmap;
    }

    private static Size CalculateWordSize(Graphics graphics, ViewWord viewWord)
    {
        var textSize = graphics.MeasureString(viewWord.Word, viewWord.Font);
        var viewWidth = (int) Math.Ceiling(textSize.Width);
        var viewHeight = (int) Math.Ceiling(textSize.Height);
        var viewSize = new Size(viewWidth, viewHeight);
        return viewSize;
    }

    private static void DrawTextInRectangle(Graphics graphics, ViewWord viewWord, Rectangle rectangle,
        ImageSettings imageSettings)
    {
        using var stringFormat = new StringFormat();
        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;
        using var brush = new SolidBrush(imageSettings.WordColor);
        graphics.DrawString(viewWord.Word, viewWord.Font, brush, rectangle, stringFormat);
    }
}