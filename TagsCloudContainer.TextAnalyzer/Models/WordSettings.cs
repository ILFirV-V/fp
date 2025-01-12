﻿namespace TagsCloudContainer.TextAnalyzer.Models;

public record WordSettings
{
    public IReadOnlyCollection<string> ValidSpeechParts { get; init; } = ["V", "S", "A", "ADV", "NUM"];

    public string MyStemPath { get; init; } = string.Empty;
}