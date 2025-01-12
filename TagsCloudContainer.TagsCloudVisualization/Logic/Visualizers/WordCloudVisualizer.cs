using System.Drawing;
using TagsCloudContainer.Core;
using TagsCloudContainer.Core.Extensions;
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
    public Result<None> SaveImage(Image image, FileSettings settings)
    {
        var fileNameWithFormat = $"{settings.OutputFileName}{settings.ImageFormat.FileExtensionFromToString()}";
        if (!Directory.Exists(settings.OutputPath))
        {
            return new Error($"Папки по указанному пути '{settings.OutputPath}' не существует.");
        }

        if (File.Exists(fileNameWithFormat))
        {
            return new Error($"Файл '{settings.OutputFileName}' в папке '{settings.OutputPath}' уже существует.");
        }

        var path = Path.Combine(settings.OutputPath, fileNameWithFormat);
        return Result.OfAction(() => image.Save(path, settings.ImageFormat));
    }

    public Result<Image> CreateImage(ImageSettings settings, IReadOnlyDictionary<string, int> wordCounts)
    {
        var bitmap = CreateImageMap(settings);
        return weigherWordSizer.CalculateWordSizes(wordCounts)
            .Then(viewWords =>
            {
                using var graphics = Graphics.FromImage(bitmap);
                return VisualizeWords(graphics, viewWords, settings);
            })
            .Then(Image (_) => bitmap);
    }

    private static Bitmap CreateImageMap(ImageSettings imageSettings)
    {
        var bitmap = new Bitmap(imageSettings.Size.Width, imageSettings.Size.Height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(imageSettings.BackgroundColor);
        return bitmap;
    }

    private Result<None> VisualizeWords(Graphics graphics, IReadOnlyCollection<ViewWord> viewWords,
        ImageSettings imageSettings)
    {
        return containerCreator.GetLayouter()
            .Then(container =>
            {
                return viewWords
                    .Select(viewWord =>
                        CalculateWordSize(graphics, viewWord)
                            .Then(container.PutNextRectangle)
                            .Then(rectangle => DrawTextInRectangle(graphics, viewWord, rectangle, imageSettings)))
                    .FirstOrDefault(result => result.IsFail);
            });
    }

    private static Result<Size> CalculateWordSize(Graphics graphics, ViewWord viewWord)
    {
        var textSize = graphics.MeasureString(viewWord.Word, viewWord.Font);
        var viewWidth = (int) Math.Ceiling(textSize.Width);
        var viewHeight = (int) Math.Ceiling(textSize.Height);
        var viewSize = new Size(viewWidth, viewHeight);
        return viewSize;
    }

    private static Result<None> DrawTextInRectangle(Graphics graphics, ViewWord viewWord, Rectangle rectangle,
        ImageSettings imageSettings)
    {
        using var stringFormat = new StringFormat();
        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;
        using var brush = new SolidBrush(imageSettings.WordColor);
        graphics.DrawString(viewWord.Word, viewWord.Font, brush, rectangle, stringFormat);
        return Result.Ok();
    }
}