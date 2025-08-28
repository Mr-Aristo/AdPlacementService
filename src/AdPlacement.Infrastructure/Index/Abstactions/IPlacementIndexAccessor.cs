using AdReplacement.Domain.Models;

namespace AdPlacement.Infrastructure.Index.Abstactions;

public interface IPlacementIndexAccessor
{
    PlatformIndex Current { get; }

    void Swap(PlatformIndex next);
}
