using System.Drawing;
using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers.Interfaces;

public interface IWordsCloudVisualizer
{
    public Result<Image> CreateImage(ImageSettings settings, IReadOnlyDictionary<string, int> wordCounts);

    public Result<None> SaveImage(Image image, FileSettings settings);
}