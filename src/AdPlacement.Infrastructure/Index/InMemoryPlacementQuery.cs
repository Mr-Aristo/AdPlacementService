using AdPlacement.Domain.Abstractions;
using AdPlacement.Infrastructure.Index.Abstactions;
using AdPlacementDomain.Models;
using AdPlacementDomain.Models.Dtos;


namespace AdPlacement.Infrastructure.Index;

public sealed class InMemoryPlacementQuery : IPlacementQuery
{
    private readonly IPlacementIndexAccessor _accessor;

    public InMemoryPlacementQuery(IPlacementIndexAccessor accessor)
    {
        _accessor = accessor;
    }

    public Task<PlatformsResult> GetPlatformsAsync(string location, CancellationToken ct = default)
    {
        var idx = _accessor.Current;
        var normalized = LocationPath.Normalize(location);
        var segs = LocationPath.SplitSegments(normalized);


        var node = idx.Root;
        int matched = 0;
        foreach (var s in segs)
        {
            if (!node.Children.TryGetValue(s, out var next))
                break;
            node = next;
            matched++;
        }


        var names = idx.MapIdsToNames(node.EffectivePlatformIds);
        return Task.FromResult(new PlatformsResult(normalized, names, matched));
    }
}

