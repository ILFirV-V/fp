﻿using TagsCloudContainer.Core;
using TagsCloudContainer.TagsCloudVisualization.Models;

namespace TagsCloudContainer.TagsCloudVisualization.Logic.SizeCalculators.Interfaces;

public interface IWeigherWordSizer
{
    public Result<IReadOnlyCollection<ViewWord>> CalculateWordSizes(IReadOnlyDictionary<string, int> wordFrequencies,
        int minSize = 8, int maxSize = 24);
}