using AdPlacement.Infrastructure.Index.Abstactions;
using AdReplacement.Domain.Models;

namespace AdPlacement.Infrastructure.Index;

public sealed class PlacementIndexAccessor : IPlacementIndexAccessor
{
    private PlatformIndex _current = PlatformIndex.Empty;
    public PlatformIndex Current => Volatile.Read(ref _current);

    public void Swap(PlatformIndex next) => Interlocked.Exchange(ref _current, next);
}
