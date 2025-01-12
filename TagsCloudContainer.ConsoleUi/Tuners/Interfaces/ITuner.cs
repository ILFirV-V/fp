using TagsCloudContainer.Core;

namespace TagsCloudContainer.ConsoleUi.Tuners.Interfaces;

public interface ITuner
{
    public Result<None> Tune(string[] arguments);
}