using TagsCloudContainer.Core;

namespace TagsCloudContainer.ConsoleUi.Runner.Interfaces;

public interface ITagsCloudContainerUi
{
    public Result<None> Run(string[] args);
}