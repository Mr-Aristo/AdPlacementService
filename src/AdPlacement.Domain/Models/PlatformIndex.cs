

using System;

namespace AdPlacementDomain.Models;

public sealed class PlatformIndex
{
    public TrieNode Root { get; }
    public string[] PlatformNamesById { get; }

    public bool IsLoaded { get; }
    public int NodeCount { get; }
    public int PlatformCount { get; }
    public int SkippedLineCount { get; }
    public DateTimeOffset LoadedAt { get; }
    public static PlatformIndex Empty { get; } = new(new TrieNode(), Array.Empty<string>(), false, 1, 0, 0, DateTimeOffset.MinValue);
    public PlatformIndex(TrieNode root, string[] namesById, bool isLoaded, int nodeCount, int platformCount, int skippedLineCount, DateTimeOffset loadedAt)
    {
        Root = root;
        PlatformNamesById = namesById;
        IsLoaded = isLoaded;
        NodeCount = nodeCount;
        PlatformCount = platformCount;
        SkippedLineCount = skippedLineCount;
        LoadedAt = loadedAt;
    }
    public IReadOnlyList<string> MapIdsToNames(IReadOnlyList<int> ids)
    {
        var result = new string[ids.Count];
        for (int i = 0; i < ids.Count; i++) result[i] = PlatformNamesById[ids[i]];
        return result;
    }
}
