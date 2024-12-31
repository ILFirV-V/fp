using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Models.Settings;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.Visualizers.Interfaces;

public interface IWordsCloudVisualizer
{
    public Image CreateImage(ImageSettings settings, IReadOnlyDictionary<string, int> wordCounts);

    public void SaveImage(Image image, FileSettings settings);
}